using Application.Interfaces.Repositories;
using Common.Exceptions;
using Common.Models.HelperInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories;

public class GenericCrudAndArchiveRepository<TModel> : GenericCrudRepository<TModel>,
    IArchiveableEntityRepository<TModel>
    where TModel : class, IBaseEntity, IArchiveable
{
    private readonly DbContext context;

    public GenericCrudAndArchiveRepository(DbContext context) : base(context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<List<TModel>> GetAllArchived(CancellationToken cancellationToken = default)
    {
        return context.Set<TModel>().Where(e => e.ArchivedDate != null).ToListAsync(cancellationToken);
    }

    public async Task Archive(Guid id, CancellationToken cancellationToken = default)
    {
        var entityToArchive =
            await context.Set<TModel>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entityToArchive == null) throw new EntityNotFoundException(id, typeof(TModel));
        entityToArchive.IsArchived = true;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Unarchive(Guid id, CancellationToken cancellationToken = default)
    {
        var entityToUnarchive =
            await context.Set<TModel>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entityToUnarchive == null) throw new EntityNotFoundException(id, typeof(TModel));
        entityToUnarchive.IsArchived = false;
        entityToUnarchive.IsDeleted = false;
        await context.SaveChangesAsync(cancellationToken);
    }

    public new Task<List<TModel>> GetAllActive(CancellationToken cancellationToken = default)
    {
        return context.Set<TModel>().Where(e => e.ArchivedDate == null && e.DeletedDate == null).ToListAsync(cancellationToken);
    }
}