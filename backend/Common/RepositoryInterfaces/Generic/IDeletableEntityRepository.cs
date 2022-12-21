using Common.Models.HelperInterfaces;

namespace Common.RepositoryInterfaces.Generic;

public interface IDeletableEntityRepository<TModel>
    where TModel : DeletableBaseEntity
{
    public Task<List<TModel>> GetAllActiveAsync(CancellationToken cancellationToken = default);
    public Task<List<TModel>> GetAllDeletedAsync(CancellationToken cancellationToken = default);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    public Task UndeleteAsync(Guid id, CancellationToken cancellationToken = default);
}