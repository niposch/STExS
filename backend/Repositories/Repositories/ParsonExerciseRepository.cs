using Common.Models.ExerciseSystem.Parson;
using Common.RepositoryInterfaces.Tables;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.GenericImplementations;

namespace Repositories.Repositories;

public class ParsonExerciseRepository : GenericCrudRepository<ParsonExercise>, IParsonExerciseRepository
{
    public ParsonExerciseRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<ParsonExercise?> TryGetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return this.context.ParsonExercises
            .Include(x => x.ExpectedSolution)
            .ThenInclude(x => x.CodeElements)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<ParsonExercise> UpdateAsync(ParsonExercise entity, CancellationToken cancellationToken = default)
    {
        this.context.RemoveLocalIfTracked(entity);
        this.context.Entry(entity.ExpectedSolution).State = EntityState.Modified;
        this.context.ParsonExercises.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
        return entity;
    }
}