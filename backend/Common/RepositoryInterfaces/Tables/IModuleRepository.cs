using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Generic;

namespace Common.RepositoryInterfaces.Tables;

public interface IModuleRepository: IGenericCrudAndArchiveableEntityRepository<Module>
{
    Task<IEnumerable<Module>> GetModulesUserIsOwnerOfAsync(Guid ownerId, CancellationToken cancellationToken = default);
}