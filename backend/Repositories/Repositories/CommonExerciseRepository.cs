using Common.Exceptions;
using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Tables;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories;

public class CommonExerciseRepository : ICommonExerciseRepository
{
    private readonly ApplicationDbContext context;

    public CommonExerciseRepository(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<BaseExercise?> TryGetByIdAsync(Guid exerciseId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        var query = this.context.Exercises.AsQueryable();
        if (asNoTracking) query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(e => e.Id == exerciseId, cancellationToken);
    }

    public async Task<List<BaseExercise>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await this.context.Exercises.ToListAsync(cancellationToken);
    }

    public async Task<List<BaseExercise>> GetForChapterAsync(Guid chapterId, CancellationToken cancellationToken = default)
    {
        return await this.context.Exercises
            .Where(e => e.ChapterId == chapterId)
            .OrderBy(e => e.RunningNumber)
            .ToListAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var exercise = await this.TryGetByIdAsync(exerciseId, false, cancellationToken);
        if (exercise is null) throw new EntityNotFoundException<BaseExercise>(exerciseId);

        this.context.Exercises.Remove(exercise);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<BaseExercise>> SearchAsync(string search, CancellationToken cancellationToken = default)
    {
        var searchWords = search.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var query = this.context.Exercises
            .AsQueryable();
        foreach (var w in searchWords)
            query = query.Where(e =>
                e.Description.ToLower().Contains(w) ||
                e.ExerciseName.ToLower().Contains(w)
            );

        var results = await query.ToListAsync(cancellationToken);

        return results
            .OrderByDescending(e => e.CreationTime)
            .ToList();
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