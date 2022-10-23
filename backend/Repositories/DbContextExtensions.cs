using Common.Models.HelperInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public static class DbContextExtensions
{
    public static void RemoveLocalIfTracked<T>(this DbContext context, T entity)
        where T : class, IBaseEntity
    {
        var local = context.Set<T>().Local.FirstOrDefault(e => e.Id == entity.Id);
        if (local != null) context.Entry(local).State = EntityState.Detached;
    }
}