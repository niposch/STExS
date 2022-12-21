using Common.Models.Authentication;
using Common.Models.ExerciseSystem;

namespace Application.Services.Interfaces;

public interface IModuleService
{
    public Task CreateModuleAsync(string moduleName,
        string moduleDescription,
        Guid ownerId,
        CancellationToken cancellationToken = default);
    public Task UpdateModuleAsync(Module module, CancellationToken cancellationToken = default);
    public Task DeleteModuleAsync(Guid moduleId, CancellationToken cancellationToken = default);
    public Task ArchiveModuleAsync(Guid moduleId, CancellationToken cancellationToken = default);
    
    public Task<Module> GetModuleByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<IEnumerable<Module>> GetModulesAsync(CancellationToken cancellationToken = default);
    
    public Task<IEnumerable<Module>> GetModulesUserIsAcceptedIntoAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Module>> GetArchivedModulesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Module>> GetUsersInvitedToModuleAsync(Guid moduleId, CancellationToken cancellationToken = default);
    Task UnarchiveModuleAsync(Guid moduleId, CancellationToken cancellationToken = default);
}