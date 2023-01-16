using Application.DTOs.ModuleDTOs;
using Application.Helper.Roles;
using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.Authentication;
using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Generic;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

// TODO add unit tests for this service
public sealed class ModuleService : IModuleService
{
    private readonly IApplicationRepository repository;
    private readonly UserManager<ApplicationUser> userManager;

    public ModuleService(IApplicationRepository repository, 
        UserManager<ApplicationUser> userManager)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task CreateModuleAsync(string moduleName,
        string moduleDescription,
        int newMaxParticipants,
                Guid ownerId,
        CancellationToken cancellationToken = default)
    {
        var module = new Module
        {
            ArchivedDate = null,
            IsArchived = false,
            ModuleParticipations = new List<ModuleParticipation>(),
            Chapters = new List<Chapter>(),
            OwnerId = ownerId,
            ModuleName = moduleName,
            ModuleDescription = moduleDescription,
            MaxParticipants = newMaxParticipants
        };
        await this.repository.Modules.AddAsync(module, cancellationToken);
    }

    public async Task UpdateModuleAsync(Guid moduleId,
        string newName,
        string newDescription,
        int? newMaxParticipants,
        CancellationToken cancellationToken = default)
    {
        var module = await this.repository.Modules.TryGetByIdAsync(moduleId, cancellationToken);
        if (module == null) throw new EntityNotFoundException<Module>(moduleId);

        module.ModuleName = newName;
        module.ModuleDescription = newDescription;
        module.MaxParticipants = newMaxParticipants;
        await this.repository.Modules.UpdateAsync(module, cancellationToken);
    }

    public async Task DeleteModuleAsync(Guid moduleId, Guid userId, CancellationToken cancellationToken = default)
    {
        var module = (await this.repository.Modules.TryGetByIdAsync(moduleId, cancellationToken)) ?? throw new EntityNotFoundException<Module>(moduleId);
        // Check if user is allowed to delete module
        var user = await this.userManager.FindByIdAsync(userId.ToString());
        if (user == null) 
            throw new UnauthorizedException();
        
        var roles = this.userManager.GetRolesAsync(user);
        
        if (!roles.Result.Contains(RoleHelper.Admin) && !roles.Result.Contains(RoleHelper.Teacher))
            throw new UnauthorizedException();
        if (roles.Result.Contains(RoleHelper.Teacher) && module.OwnerId != userId) 
            throw new UnauthorizedException();
        
        await this.repository.Modules.DeleteAsync(moduleId, cancellationToken);
    }

    public async Task ArchiveModuleAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
        await this.repository.Modules.ArchiveAsync(moduleId, cancellationToken);
    }

    public async Task UnarchiveModuleAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
        await this.repository.Modules.UnarchiveAsync(moduleId, cancellationToken);
    }

    public async Task<List<Module>> GetModulesUserIsAdminOfAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var modules = await this.repository.Modules.GetModulesUserIsOwnerOfAsync(userId, cancellationToken);
        return modules.ToList();
    }

    public async Task<List<Module>> SearchModulesAsync(string search, CancellationToken cancellationToken = default)
    {
        var modules = await this.repository.Modules.GetAllAsync(cancellationToken);
        return modules
            .Where(m => Search(search, m))
            .OrderByDescending(m => m.IsArchived)
            .ThenByDescending(m => m.CreationTime)
            .ToList();
    }

    public async Task<JoinResult> JoinModuleAsync(Guid moduleId, Guid userId, CancellationToken cancellationToken = default)
    {
        var existingParticipation = await this.repository.ModuleParticipations.TryGetByModuleAndUserIdAsync(moduleId, userId, cancellationToken);
        if (existingParticipation != null) return existingParticipation.ParticipationConfirmed ? JoinResult.AlreadyJoined : JoinResult.VerificationPending;
        
        var newParticipation = new ModuleParticipation
        {
            ModuleId = moduleId,
            UserId = userId,
            ParticipationConfirmed = false
        };
        var module = await this.repository.Modules.TryGetByIdAsync(moduleId, cancellationToken);
        if (module == null) return JoinResult.ModuleDoesNotExist;
        if (module.OwnerId == userId) return JoinResult.UserIsOwner;
        if(module.IsArchived) return JoinResult.ModuleIsArchived;
        if (module.MaxParticipants != null)
        {
            var participationCount = await this.repository.ModuleParticipations.GetParticipationCountByModuleIdAsync(moduleId, cancellationToken);
            if (participationCount >= module.MaxParticipants) return JoinResult.ModuleIsFull;
        }
        
        await this.repository.ModuleParticipations.AddAsync(newParticipation, cancellationToken);
        return JoinResult.JoinedSucessfully;
    }

    public async Task<ModuleParticipationStatus> GetModuleParticipationStatusAsync(Guid moduleId, Guid userId, CancellationToken cancellationToken)
    {
        var module = await this.repository.Modules.TryGetByIdAsync(moduleId, cancellationToken);
        if (module == null) throw new EntityNotFoundException<Module>(moduleId);
        if (module.OwnerId == userId) return ModuleParticipationStatus.Admin;
        var participation = await this.repository.ModuleParticipations.TryGetByModuleAndUserIdAsync(moduleId, userId, cancellationToken);
        if (participation == null) return ModuleParticipationStatus.NotParticipating;
        return participation.ParticipationConfirmed ? ModuleParticipationStatus.Accepted : ModuleParticipationStatus.Requested;
    }

    public async Task<int?> GetModuleParticipantCountAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
        return await this.repository.ModuleParticipations.GetParticipationCountByModuleIdAsync(moduleId, cancellationToken);
    }

    public async Task<List<ModuleParticipationDetailItem>> GetParticipationsForModuleAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
        var moduleParticipations = await this.repository.ModuleParticipations.GetParticipationsForModuleAsync(moduleId);

        return moduleParticipations.Select(e => ModuleParticipationMapper.ToDetailItem(e))
            .ToList();
    }

    public async Task ConfirmModuleParticipationAsync(Guid moduleId, Guid userId, CancellationToken cancellationToken = default)
    {
        await this.repository.ModuleParticipations.TryConfirmParticipationAsync(userId, moduleId, cancellationToken);
    }

    public async Task<List<ModuleParticipationDetailItem>> GetParticipationsForAdminUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var moduleParticipations = await this.repository.ModuleParticipations.GetParticipationsForAdminToAcceptAsync(userId, cancellationToken);
        
        return moduleParticipations.Select(e => ModuleParticipationMapper.ToDetailItem(e))
            .ToList();
    }

    public async Task RejectModuleParticipationAsync(Guid moduleId, Guid userId, CancellationToken cancellationToken = default)
    {
        await this.repository.ModuleParticipations.RemoveAsync(moduleId, userId, cancellationToken);
    }

    public async Task<Module> GetModuleByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await this.repository.Modules.TryGetByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException<Module>(id);
    }

    public async Task<IEnumerable<Module>> GetModulesAsync(CancellationToken cancellationToken = default)
    {
        return await this.repository.Modules.GetAllAsync(cancellationToken);
    }

    public async Task<IEnumerable<Module>> GetArchivedModulesAsync(CancellationToken cancellationToken = default)
    {
        return await this.repository.Modules.GetAllArchivedAsync(cancellationToken);
    }

    public async Task<IEnumerable<Module>> GetActiveModulesAsync(CancellationToken cancellationToken = default)
    {
        return await this.repository.Modules.GetAllActiveAsync(cancellationToken);
    }

    public async Task<IEnumerable<Module>> GetModulesUserIsAcceptedIntoAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var participationsForUser = await this.repository.ModuleParticipations.GetParticipationsForUserAsync(userId, cancellationToken);
        return participationsForUser
            .Where(p => p.ParticipationConfirmed)
            .Select(p => p.Module)
            .DistinctBy(m => m.Id);
    }

    public async Task<IEnumerable<Module>> GetParticipationsForUserAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
        var participationsForModule = await this.repository.ModuleParticipations.GetParticipationsForUserAsync(moduleId);
        return participationsForModule.Select(p => p.Module);
    }
    
    private static bool Search(string search, Module module)
    {
        var words = search.Split(" ");
        return words.All(word => module.ModuleName.Contains(word,
                                     StringComparison.OrdinalIgnoreCase) ||
                                 module.ModuleDescription.Contains(word,
                                     StringComparison.OrdinalIgnoreCase) ||
                                 (module.Owner?.UserName.Contains(word,
                                     StringComparison.OrdinalIgnoreCase) ?? false) ||
                                 (module.Owner?.FirstName.Contains(word) ?? false) ||
                                 (module.Owner?.LastName.Contains(word) ?? false));
    }
}