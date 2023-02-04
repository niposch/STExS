﻿using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories.Grading;

// TODO AUFGABE LEI Methoden implementieren + Unit Tests in Repositories.Tests/Repositories/UserSubmissionRepositoryTests/*
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
        var ex = await this.context.UserSubmissions.FirstOrDefaultAsync(e => e.ExerciseId == exerciseId,
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

    public async Task<IGrouping<Guid, UserSubmission>> GetAllByExerciseIdGroupedByUserIdAsync(Guid exerciseId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}