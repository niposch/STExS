using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.Authentication;
using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Generic;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

// TODO add unit tests for this service
public sealed class ModuleService: IModuleService
{
    private readonly IApplicationRepository repository;

    public ModuleService(IApplicationRepository repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
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

    public async Task UpdateModuleAsync(Guid moduleId, string newName, string newDescription, CancellationToken cancellationToken = default)
    {
        var module = await this.repository.Modules.TryGetByIdAsync(moduleId, cancellationToken);
        if (module == null)
        {
            throw new EntityNotFoundException<Module>(moduleId);
        }

        module.ModuleName = newName;
        module.ModuleDescription = newDescription;
        await this.repository.Modules.UpdateAsync(module, cancellationToken);
    }

    public async Task DeleteModuleAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
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
        var participationsForUser =  await this.repository.ModuleParticipations.GetParticipationsForUserAsync(userId);
        var ownerOfModules = await this.repository.Modules.GetModulesUserIsOwnerOfAsync(userId);
        return ownerOfModules.Concat(participationsForUser
                .Where(p => p.ParticipationConfirmed)
                .Select(p => p.Module))
            .DistinctBy(m => m.Id);
    }
    
    public async Task<IEnumerable<Module>> GetParticipationsForUserAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
        var participationsForModule =  await this.repository.ModuleParticipations.GetParticipationsForUserAsync(moduleId);
        return participationsForModule.Select(p => p.Module);
    }
}