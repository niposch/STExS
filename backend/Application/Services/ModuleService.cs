using Application.Services.Interfaces;
using Common.Models.ExerciseSystem;

namespace Application.Services;

public sealed class ModuleService: IModuleService
{
    public async Task CreateModuleAsync(Module module, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateModuleAsync(Module module, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteModuleAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task ArchiveModuleAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Module> GetModuleByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Module>> GetModulesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Module>> GetModulesUserIsAcceptedInto(Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}