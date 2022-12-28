using Application.DTOs.Module;
using Common.Models.ExerciseSystem;

namespace Application.Services.Interfaces;

public interface IModuleService
{
    public Task CreateModuleAsync(string moduleName,
        string moduleDescription,
        Guid ownerId,
        CancellationToken cancellationToken = default);

    public Task UpdateModuleAsync(Guid moduleId,
        string newName,
        string newDescription,
        CancellationToken cancellationToken = default);

    public Task DeleteModuleAsync(Guid moduleId, Guid userId, CancellationToken cancellationToken = default);

    public Task<Module> GetModuleByIdAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<IEnumerable<Module>> GetModulesAsync(CancellationToken cancellationToken = default);
    public Task<IEnumerable<Module>> GetArchivedModulesAsync(CancellationToken cancellationToken = default);
    public Task<IEnumerable<Module>> GetActiveModulesAsync(CancellationToken cancellationToken = default);


    public Task<IEnumerable<Module>> GetModulesUserIsAcceptedIntoAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Module>> GetParticipationsForUserAsync(Guid moduleId, CancellationToken cancellationToken = default);

    public Task ArchiveModuleAsync(Guid moduleId, CancellationToken cancellationToken = default);
    public Task UnarchiveModuleAsync(Guid moduleId, CancellationToken cancellationToken = default);
    Task<List<Module>> GetModulesUserIsAdminOfAsync(Guid userId, CancellationToken cancellationToken);
    Task<List<Module>> SearchModulesAsync(string search, CancellationToken cancellationToken);

    Task<JoinResult> JoinModuleAsync(Guid moduleId, Guid userId, CancellationToken cancellationToken = default);
    Task<ModuleParticipationStatus> GetModuleParticipationStatusAsync(Guid moduleId, Guid userId, CancellationToken cancellationToken);
}