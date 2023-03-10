using Common.Models.ExerciseSystem.Cloze;
using Common.Models.ExerciseSystem.Parson;
using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;
using Common.RepositoryInterfaces.Tables;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories.Grading;

// TODO AUFGABE LEI Unit Tests in Repositories.Tests/Repositories/UserSubmissionRepositoryTests/*
public class UserSubmissionRepository : IUSerSubmissionRepository
{
    private readonly ApplicationDbContext context;

    public UserSubmissionRepository(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<UserSubmission?> TryGetByIdAsync(Guid userId, Guid exerciseId,
        CancellationToken cancellationToken = default)
    {
        //Include submissions twice so both AnsweItem types will be fetched; EF should consolidate joins according to docs.
        var ex = await this.context.UserSubmissions
            .Include(s => s.Submissions)
            .ThenInclude(s => (s as ParsonPuzzleSubmission).AnswerItems)
            .ThenInclude(a => a.ParsonElement)
            .Include(s => s.Submissions)
            .ThenInclude(s => (s as ClozeTextSubmission).SubmittedAnswers)
            .FirstOrDefaultAsync(e => e.ExerciseId == exerciseId && e.UserId == userId,
            cancellationToken);
        return ex;
    }

    public async Task UpdateAsync(UserSubmission userSubmission, CancellationToken cancellationToken = default)
    {
        this.context.UserSubmissions.Update(userSubmission);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateAsync(UserSubmission userSubmission, CancellationToken cancellationToken = default)
    {
        await this.context.UserSubmissions.AddAsync(userSubmission, cancellationToken);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(UserSubmission userSubmission, CancellationToken cancellationToken = default)
    {
        this.context.UserSubmissions.Remove(userSubmission);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<UserSubmission>> GetAllByUserIdAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        var ex = await this.context.UserSubmissions.Where(e => e.UserId == userId).ToListAsync(cancellationToken);
        return ex;
    }
    
    public async Task<Dictionary<Guid, UserSubmission>> GetAllByExerciseIdGroupedByUserIdAsync(Guid exerciseId,
        CancellationToken cancellationToken = default)
    {
        return await this.context.UserSubmissions
            .Where(e => e.ExerciseId == exerciseId)
            .Include(e => e.FinalSubmission)
            .ThenInclude(s => s.GradingResult)
            .Include(e => e.Submissions)
            .ThenInclude(s => s.GradingResult)
            .Include(s => s.TimeTracks)
            .ToDictionaryAsync(e => e.UserId, cancellationToken);
    }

    public int ExistsByExerciseIdAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        return this.context.UserSubmissions
            .Count(s => s.ExerciseId == exerciseId);
    }

    public async Task<List<UserSubmission>> GetAllByUserIdAndChapterAsync(Guid chapterId, Guid userId, CancellationToken cancellationToken = default)
    {
        var userSubmissions = await this.context.UserSubmissions
            .Include(s => s.FinalSubmission)
            .ThenInclude(s => s.GradingResult)
            .Include(s => s.Exercise)
            .Where(s => s.UserId == userId && s.Exercise.ChapterId == chapterId)
            .ToListAsync(cancellationToken);

        return userSubmissions;
    }
}