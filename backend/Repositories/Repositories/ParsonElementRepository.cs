using Common.Models.ExerciseSystem.Parson;
using Common.RepositoryInterfaces.Tables;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.GenericImplementations;

namespace Repositories.Repositories;

public class ParsonElementRepository : GenericCrudRepository<ParsonElement>, IParsonElementRepository
{
    public ParsonElementRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task RemoveRangeAsync(List<ParsonElement> linesToDelete, CancellationToken cancellationToken = default)
    {
        this.context.ParsonElements.RemoveRange(linesToDelete);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ParsonElement element, CancellationToken cancellationToken = default)
    {
        this.context.RemoveLocalIfTracked(element);
        this.context.ParsonElements.Update(element);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAsync(ParsonElement last, CancellationToken cancellationToken = default)
    {
        this.context.RemoveLocalIfTracked(last);
        await this.context.ParsonElements.AddAsync(last, cancellationToken);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<ParsonElement>> GetForExerciseAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var elements = await this.context.ParsonElements
            .Where(x => x.RelatedSolution.RelatedExerciseId == exerciseId)
            .ToListAsync(cancellationToken);
        return elements;
    }
}