﻿using Common.Exceptions;
using Common.Models.HelperInterfaces;
using Common.RepositoryInterfaces.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories.GenericImplementations;

public class GenericCrudAndArchiveRepository<TModel> : GenericCrudRepository<TModel>,
    IGenericCrudAndArchiveableEntityRepository<TModel>
    where TModel : ArchiveableBaseEntity
{
    private readonly ApplicationDbContext context;

    public GenericCrudAndArchiveRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<TModel>> GetAllActiveAsync(CancellationToken cancellationToken = default)
    {
        return this.context.Set<TModel>().Where(m => m.ArchivedDate == null).ToList();
    }

    public Task<List<TModel>> GetAllArchivedAsync(CancellationToken cancellationToken = default)
    {
        return context.Set<TModel>().Where(e => e.ArchivedDate != null).ToListAsync(cancellationToken);
    }

    public async Task ArchiveAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var entityToArchive =
            await context.Set<TModel>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entityToArchive == null) throw new EntityNotFoundException<TModel>(id);
        if (!entityToArchive.IsArchived)
        {
            entityToArchive.IsArchived = true;
        }
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UnarchiveAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var entityToUnarchive =
            await context.Set<TModel>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entityToUnarchive == null) throw new EntityNotFoundException<TModel>(id);
        if (entityToUnarchive.IsArchived)
        {
            entityToUnarchive.IsArchived = false;
        }
        await context.SaveChangesAsync(cancellationToken);
    }
}