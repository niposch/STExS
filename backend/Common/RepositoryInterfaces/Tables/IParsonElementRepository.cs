using Common.Models.ExerciseSystem.Parson;

namespace Common.RepositoryInterfaces.Tables;

public interface IParsonElementRepository
{
    Task RemoveRangeAsync(List<ParsonElement> linesToDelete, CancellationToken cancellationToken = default);
    Task UpdateAsync(ParsonElement element, CancellationToken cancellationToken = default);
    Task AddAsync(ParsonElement last, CancellationToken cancellationToken = default);
}