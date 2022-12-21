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
}