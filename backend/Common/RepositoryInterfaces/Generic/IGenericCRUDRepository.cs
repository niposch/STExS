using Common.Models.HelperInterfaces;

namespace Common.RepositoryInterfaces.Generic;

public interface IGenericCrudRepository<TModel> : IDeletableEntityRepository<TModel>
    where TModel : DeletableBaseEntity
{
    Task<TModel?> TryGetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TModel> AddAsync(TModel entity, CancellationToken cancellationToken = default);
    Task<TModel> UpdateAsync(TModel entity, CancellationToken cancellationToken = default);
}