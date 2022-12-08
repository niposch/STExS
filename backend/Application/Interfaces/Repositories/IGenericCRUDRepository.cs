using Common.Models.HelperInterfaces;

namespace Application.Interfaces.Repositories;

public interface IGenericCrudRepository<TModel> : IDeletableEntityRepository<TModel>
    where TModel : class, IBaseEntity, IDeletable
{
    Task<TModel?> TryGetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TModel> AddAsync(TModel entity, CancellationToken cancellationToken = default);
    Task<TModel> UpdateAsync(TModel entity, CancellationToken cancellationToken = default);
}