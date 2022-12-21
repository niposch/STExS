using Common.Models.HelperInterfaces;

namespace Common.RepositoryInterfaces.Generic;

public interface IGenericCrudAndArchiveableEntityRepository<TModel> : IGenericCrudRepository<TModel>
    where TModel : class, IBaseEntity, IArchiveable, IDeletable
{
    public new Task<List<TModel>> GetAllActiveAsync(CancellationToken cancellationToken = default);
    public Task<List<TModel>> GetAllArchivedAsync(CancellationToken cancellationToken = default);
    public Task ArchiveAsync(Guid id, CancellationToken cancellationToken = default);
    public Task UnarchiveAsync(Guid id, CancellationToken cancellationToken = default);
}