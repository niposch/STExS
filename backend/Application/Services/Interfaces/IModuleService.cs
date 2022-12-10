using Common.Models.ExerciseSystem;

namespace Application.Services.Interfaces;

public interface IModuleService
{
    public Task CreateModuleAsync(Module module, CancellationToken cancellationToken = default);
    public Task UpdateModuleAsync(Module module, CancellationToken cancellationToken = default);
    public Task DeleteModuleAsync(Guid moduleId, CancellationToken cancellationToken = default);
    public Task ArchiveModuleAsync(Guid moduleId, CancellationToken cancellationToken = default);
    
    public Task<Module> GetModuleByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<IEnumerable<Module>> GetModulesAsync(CancellationToken cancellationToken = default);
    
    public Task<IEnumerable<Module>> GetModulesUserIsAcceptedInto(Guid userId, CancellationToken cancellationToken = default);
}