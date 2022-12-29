using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Generic;

namespace Common.RepositoryInterfaces.Tables;

public interface IChapterRepository: IGenericCrudRepository<Chapter>
{
    Task<List<Chapter>> GetChaptersByModuleIdAsync(Guid moduleId, CancellationToken cancellationToken = default);
}