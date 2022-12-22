using Common.Models.HelperInterfaces;

namespace Common.RepositoryInterfaces.Generic;

public interface IDeletableEntityRepository<TModel>
    where TModel : DeletableBaseEntity
{
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}