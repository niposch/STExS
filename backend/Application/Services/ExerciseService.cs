﻿using System.Xml;
using Application.DTOs.ExercisesDTOs;
using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services;

public sealed class ExerciseService: IExerciseService
{
    private readonly IApplicationRepository repository;

    public ExerciseService(IApplicationRepository repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<ExerciseDetailItem> CopyToChapterAsync(Guid existingExerciseId, Guid chapterToCopyTo, CancellationToken cancellationToken = default)
    {
        var exercise = await this.repository.CommonExercises.TryGetByIdAsync(existingExerciseId,true, cancellationToken);
        if (exercise is null)
        {
            throw new ArgumentException($"Exercise with id {existingExerciseId} does not exist");
        }
        
        var nextAvailableRunningNumber = (await this.repository.CommonExercises.GetForChapterAsync(chapterToCopyTo, cancellationToken))
            .Select(m => m.RunningNumber)
            .DefaultIfEmpty(0)
            .Max() + 1;

        exercise.Id = Guid.NewGuid();
        
        exercise.ChapterId = chapterToCopyTo;
        exercise.Chapter = null!;
        exercise.RunningNumber = nextAvailableRunningNumber;
        
        exercise = await this.repository.CommonExercises.AddAsync(exercise, cancellationToken);

        return ToDetailItem(exercise);
    }

    public async Task<List<ExerciseDetailItem>> GetByChapterIdAsync(Guid chapterId, CancellationToken cancellationToken = default)
    {
        var exercises = await this.repository.CommonExercises.GetForChapterAsync(chapterId, cancellationToken);

        return exercises.Select(e => ToDetailItem(e))
            .ToList();
    }

    public async Task DeleteExerciseAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        await this.repository.CommonExercises.DeleteAsync(exerciseId, cancellationToken);
    }

    public async Task<ExerciseDetailItem> GetExerciseByIdAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var exercise = await this.repository.CommonExercises.TryGetByIdAsync(exerciseId, false, cancellationToken) ?? throw new EntityNotFoundException<BaseExercise>(exerciseId);

        return ToDetailItem(exercise);
    }

    public async Task<List<ExerciseDetailItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var all = await this.repository.CommonExercises.GetAllAsync(cancellationToken);

        return all
            .Select(e => ToDetailItem(e))
            .ToList();
    }

    private static ExerciseDetailItem ToDetailItem(BaseExercise entity)
    {
        return new ExerciseDetailItem
        {
            Id = entity.Id,
            ExerciseName = entity.ExerciseName,
            ChapterId = entity.ChapterId,
            AchieveablePoints = entity.AchievablePoints,
            CreationDate = entity.CreationTime,
            ExerciseDescription = entity.Description,
            ExerciseType = entity.ExerciseType,
            ModificationDate = entity.ModificationTime,
            RunningNumber = entity.RunningNumber
        };
    }
}