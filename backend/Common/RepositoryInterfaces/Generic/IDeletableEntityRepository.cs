using Common.Models.HelperInterfaces;

namespace Common.Repositories.Generic;

public interface IDeletableEntityRepository<TModel>
    where TModel : class, IDeletable
{
    public Task<List<TModel>> GetAllActive(CancellationToken cancellationToken = default);
    public Task<List<TModel>> GetAllDeleted(CancellationToken cancellationToken = default);
    public Task Delete(Guid id, CancellationToken cancellationToken = default);
    public Task Undelete(Guid id, CancellationToken cancellationToken = default);
}