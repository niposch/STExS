using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services;

// TODO add unit tests for this service
public sealed class ModuleService: IModuleService
{
    private readonly IApplicationRepository repository;

    public ModuleService(IApplicationRepository repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task CreateModuleAsync(Module module, CancellationToken cancellationToken = default)
    {
        await this.repository.Modules.AddAsync(module, cancellationToken);
    }

    public async Task UpdateModuleAsync(Module module, CancellationToken cancellationToken = default)
    {
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

    public async Task<Module> GetModuleByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await this.repository.Modules.TryGetByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException<Module>(id);
    }

    public async Task<IEnumerable<Module>> GetModulesAsync(CancellationToken cancellationToken = default)
    {
        return await this.repository.Modules.GetAllActiveAsync(cancellationToken);
    }

    public async Task<IEnumerable<Module>> GetArchivedModulesAsync(CancellationToken cancellationToken = default)
    {
        return await this.repository.Modules.GetAllArchivedAsync(cancellationToken);
    }

    public async Task<IEnumerable<Module>> GetModulesUserIsAcceptedInto(Guid userId, CancellationToken cancellationToken = default)
    {
        var participationsForUser =  await this.repository.ModuleParticipations.GetParticipationsForUser(userId);
        return participationsForUser.Select(p => p.Module);
    }
    
    public async Task<IEnumerable<Module>> GetUsersInvitedToModule(Guid moduleId, CancellationToken cancellationToken = default)
    {
        var participationsForModule =  await this.repository.ModuleParticipations.GetParticipationsForUser(moduleId);
        return participationsForModule.Select(p => p.Module);
    }
}