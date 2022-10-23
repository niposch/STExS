using Common.Models.HelperInterfaces;

namespace Application.Interfaces.Repositories;

public interface IArchiveableEntityRepository<TModel> : IDeletableEntityRepository<TModel>
    where TModel : class, IArchiveable, IDeletable
{
    public Task<List<TModel>> GetAllActive(CancellationToken cancellationToken = default);
    public Task<List<TModel>> GetAllArchived(CancellationToken cancellationToken = default);
    public Task Archive(Guid id, CancellationToken cancellationToken = default);
    public Task Unarchive(Guid id, CancellationToken cancellationToken = default);
}