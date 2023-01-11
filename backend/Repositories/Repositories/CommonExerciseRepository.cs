using Common.Exceptions;
using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Tables;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories;

public class CommonExerciseRepository: ICommonExerciseRepository
{
    private readonly ApplicationDbContext context;

    public CommonExerciseRepository(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<BaseExercise?> TryGetByIdAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        return await this.context.Exercises.FirstOrDefaultAsync(e => e.Id == exerciseId, cancellationToken);
    }

    public async Task<List<BaseExercise>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await this.context.Exercises.ToListAsync(cancellationToken);
    }

    public async Task<List<BaseExercise>> GetForChapterAsync(Guid chapterId, CancellationToken cancellationToken = default)
    {
        return await this.context.Exercises
            .Where(e => e.ChapterId == chapterId)
            .ToListAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var exercise = await this.TryGetByIdAsync(exerciseId, cancellationToken);
        if (exercise is null)
        {
            throw new EntityNotFoundException<BaseExercise>(exerciseId);
        }
        
        this.context.Exercises.Remove(exercise);
        await this.context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<BaseExercise> AddAsync(BaseExercise entity,
        CancellationToken cancellationToken = default)
    {
        this.context.RemoveLocalIfTracked(entity);
        await this.context.Exercises.AddAsync(entity, cancellationToken);
        await this.context.SaveChangesAsync(cancellationToken);
        return entity;
    }
}