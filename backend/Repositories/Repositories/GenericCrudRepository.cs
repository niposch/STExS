﻿using Application.Interfaces.Repositories;
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