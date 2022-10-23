using Common.Models.HelperInterfaces;

namespace Application.Interfaces.Repositories;

public interface IGenericCrudRepository<TModel> : IDeletableEntityRepository<TModel>
    where TModel : class, IBaseEntity, IDeletable
{
    Task<TModel?> TryGetById(Guid id, CancellationToken cancellationToken = default);
    Task<List<TModel>> GetAll(CancellationToken cancellationToken = default);
    Task<TModel> Add(TModel entity, CancellationToken cancellationToken = default);
    Task<TModel> Update(TModel entity, CancellationToken cancellationToken = default);
}