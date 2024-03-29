﻿using Common.Models.ExerciseSystem.Cloze;
using Common.Models.ExerciseSystem.Parson;
using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;
using Common.RepositoryInterfaces.Tables;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories.Grading;

// TODO AUFGABE LEI Unit Tests in Repositories.Tests/Repositories/SubmissionRepositoryTests/*
public class SubmissionRepository : ISubmissionRepository
{
    private readonly ApplicationDbContext context;

    public SubmissionRepository(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<BaseSubmission?> TryGetByIdAsync(Guid submissionId, CancellationToken cancellationToken = default)
    {
        var ex = await this.context.Submissions
            .Include(e => ((e as ClozeTextSubmission)!).SubmittedAnswers)
            .Include(e => ((e as ParsonPuzzleSubmission)!).AnswerItems)
            .ThenInclude(s => s.ParsonElement)
            .Include(e => e.GradingResult)
            .Include(s => s.UserSubmission)
            .FirstOrDefaultAsync(e => e.Id == submissionId,
            cancellationToken);
        return ex;
    }

    public async Task UpdateAsync(BaseSubmission submission, CancellationToken cancellationToken = default)
    {
        this.context.Submissions.Update(submission);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateAsync(BaseSubmission submission, CancellationToken cancellationToken = default)
    {
        await this.context.Submissions.AddAsync(submission, cancellationToken);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BaseSubmission submission, CancellationToken cancellationToken = default)
    {
        this.context.Submissions.Remove(submission);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<BaseSubmission>> GetAllBySubmissionIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var ex = await this.context.Submissions.Where(e => e.UserId == userId).ToListAsync(cancellationToken);
        return ex;
    }

    public async Task<List<BaseSubmission>> GetAllByUserIdAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        var ex = await this.context.Submissions.Where(e => e.UserId == userId).ToListAsync(cancellationToken);
        return ex;
    }

    public async Task<List<BaseSubmission>> GetAllByUserIdAndExerciseId(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var submissions = await this.context
            .Submissions
            .Where(s => s.UserId == userId && s.ExerciseId == exerciseId)
            .ToListAsync(cancellationToken);

        return submissions;
    }
}