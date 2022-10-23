using Application.Interfaces.Repositories;
using Common.Exceptions;
using Common.Models.HelperInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories;

public class GenericCrudRepository<T> : GenericDeletableEntityRepository<T>, IGenericCrudRepository<T>
    where T : class, IBaseEntity, IDeletable
{
    private readonly DbContext context;

    public GenericCrudRepository(DbContext context) : base(context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<T?> TryGetById(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Set<T>().FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<T>> GetAll(CancellationToken cancellationToken = default)
    {
        return context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T> Add(T entity, CancellationToken cancellationToken = default)
    {
        context.RemoveLocalIfTracked(entity);
        await context.Set<T>().AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T> Update(T entity, CancellationToken cancellationToken = default)
    {
        context.RemoveLocalIfTracked(entity);
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }
}

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

public class GenericDeletableEntityRepository<TModel> : IDeletableEntityRepository<TModel>
    where TModel : class, IBaseEntity, IDeletable
{
    private readonly DbContext context;

    public GenericDeletableEntityRepository(DbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<List<TModel>> GetAllActive(CancellationToken cancellationToken = default)
    {
        return context.Set<TModel>().Where(e => e.DeletedDate == null).ToListAsync(cancellationToken);
    }

    public Task<List<TModel>> GetAllDeleted(CancellationToken cancellationToken = default)
    {
        return context.Set<TModel>().Where(e => e.DeletedDate != null).ToListAsync(cancellationToken);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var entityToDelete = await context.Set<TModel>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entityToDelete == null) throw new EntityNotFoundException(id, typeof(TModel));
        entityToDelete.IsDeleted = true;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Undelete(Guid id, CancellationToken cancellationToken = default)
    {
        var entityToUndelete =
            await context.Set<TModel>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entityToUndelete == null) throw new EntityNotFoundException(id, typeof(TModel));
        entityToUndelete.IsDeleted = false;
        await context.SaveChangesAsync(cancellationToken);
    }
}