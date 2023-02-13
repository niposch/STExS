using Application.DTOs.ModuleDTOs;
using Common.Models.ExerciseSystem;

namespace Application.Services.Interfaces;

public interface IModuleService
{
    public Task CreateModuleAsync(string moduleName,
        string moduleDescription,
        int newMaxParticipants,
        Guid ownerId,
        CancellationToken cancellationToken = default);

    public Task UpdateModuleAsync(Guid moduleId,
        string newName,
        string newDescription,
        int? newMaxParticipants,
        CancellationToken cancellationToken = default);

    public Task DeleteModuleAsync(Guid moduleId, Guid userId, CancellationToken cancellationToken = default);

    public Task<ModuleDetailItem> GetModuleByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    public Task<IEnumerable<ModuleDetailItem>> GetModulesAsync(Guid userId, CancellationToken cancellationToken = default);
    public Task<IEnumerable<ModuleDetailItem>> GetArchivedModulesAsync(Guid userId, CancellationToken cancellationToken = default);
    public Task<IEnumerable<ModuleDetailItem>> GetActiveModulesAsync(Guid userId, CancellationToken cancellationToken = default);


    public Task<IEnumerable<ModuleDetailItem>> GetModulesUserIsAcceptedIntoAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ModuleDetailItem>> GetParticipationsForUserAsync(Guid userId, CancellationToken cancellationToken = default);

    public Task ArchiveModuleAsync(Guid moduleId, CancellationToken cancellationToken = default);
    public Task UnarchiveModuleAsync(Guid moduleId, CancellationToken cancellationToken = default);
    Task<List<ModuleDetailItem>> GetModulesUserIsAdminOfAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<List<ModuleDetailItem>> SearchModulesAsync(string search, Guid userId, CancellationToken cancellationToken = default);

    Task<JoinResult> JoinModuleAsync(Guid moduleId, Guid userId, CancellationToken cancellationToken = default);
    Task<ModuleParticipationStatus> GetModuleParticipationStatusAsync(Guid moduleId, Guid userId, CancellationToken cancellationToken);
    Task<int?> GetModuleParticipantCountAsync(Guid moduleId, CancellationToken cancellationToken = default);
    
    Task<List<ModuleParticipationDetailItem>> GetParticipationsForModuleAsync(Guid moduleId, CancellationToken cancellationToken = default);
    Task ConfirmModuleParticipationAsync(Guid moduleId, Guid userId, CancellationToken cancellationToken = default);
    Task<List<ModuleParticipationDetailItem>> GetParticipationsForAdminUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task RejectModuleParticipationAsync(Guid moduleId, Guid userId, CancellationToken cancellationToken = default);
}