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
    private readonly RoleManager<ApplicationRole> roleManager;

    public ModuleService(IApplicationRepository repository, 
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    public async Task CreateModuleAsync(string moduleName,
        string moduleDescription,
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
            ModuleDescription = moduleDescription
        };
        await this.repository.Modules.AddAsync(module, cancellationToken);
    }

    public async Task UpdateModuleAsync(Guid moduleId,
        string newName,
        string newDescription,
        CancellationToken cancellationToken = default)
    {
        var module = await this.repository.Modules.TryGetByIdAsync(moduleId, cancellationToken);
        if (module == null) throw new EntityNotFoundException<Module>(moduleId);

        module.ModuleName = newName;
        module.ModuleDescription = newDescription;
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

    public async Task<List<Module>> GetModulesUserIsAdminOfAsync(Guid userId, CancellationToken cancellationToken)
    {
        var modules = await this.repository.Modules.GetModulesUserIsOwnerOfAsync(userId, cancellationToken);
        return modules.ToList();
    }

    public async Task<List<Module>> SearchModulesAsync(string search, CancellationToken cancellationToken)
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
        throw new NotImplementedException();
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
        var participationsForUser = await this.repository.ModuleParticipations.GetParticipationsForUserAsync(userId);
        var ownerOfModules = await this.repository.Modules.GetModulesUserIsOwnerOfAsync(userId);
        return ownerOfModules.Concat(participationsForUser
                .Where(p => p.ParticipationConfirmed)
                .Select(p => p.Module))
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
                                     StringComparison.OrdinalIgnoreCase));
    }
}