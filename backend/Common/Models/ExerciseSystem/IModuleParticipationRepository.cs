namespace Common.Models.ExerciseSystem;

public interface IModuleParticipationRepository
{
    public Task RegisterUserInModuleAsync(Guid userId, Guid moduleId, CancellationToken cancellationToken = default);
    public Task UnregisterUserFromModuleAsync(Guid userId, Guid moduleId, CancellationToken cancellationToken = default);
    public Task<bool> IsUserRegisteredInModuleAsync(Guid userId, Guid moduleId, CancellationToken cancellationToken = default);
    public Task<IEnumerable<ModuleParticipation>> GetParticipationsForModuleAsync(Guid moduleId, CancellationToken cancellationToken = default);
    public Task<IEnumerable<ModuleParticipation>> GetParticipationsForUserAsync(Guid userId, CancellationToken cancellationToken = default);
    
    public Task TryConfirmParticipationAsync(Guid userId, Guid moduleId, CancellationToken cancellationToken = default);
    public Task TryUnconfirmParticipationAsync(Guid userId, Guid moduleId, CancellationToken cancellationToken = default);
    Task<ModuleParticipation?> TryGetByModuleAndUserIdAsync(Guid moduleId, Guid userId, CancellationToken cancellationToken);
    Task<int> GetParticipationCountByModuleIdAsync(Guid moduleId, CancellationToken cancellationToken);
    
    Task AddAsync(ModuleParticipation newParticipation, CancellationToken cancellationToken = default);
    
    Task<List<ModuleParticipation>> GetByModuleIdsAndUserIdAsync(IEnumerable<Guid> moduleIds, Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ModuleParticipation>> GetParticipationsForAdminToAcceptAsync(Guid userId, CancellationToken cancellationToken = default);
    Task RemoveAsync(Guid moduleId, Guid userId, CancellationToken cancellationToken = default);
}