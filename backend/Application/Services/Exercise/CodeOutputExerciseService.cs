using Application.DTOs.Exercises.CodeOutput;
using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.CodeOutput;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services.Exercise;

public class CodeOutputExerciseService: ICodeOutputExerciseService
{
    private readonly IApplicationRepository repository;

    public CodeOutputExerciseService(IApplicationRepository repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<CodeOutputDetailItem> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var exercise = await this.repository.CodeOutputExercises.TryGetById(id, cancellationToken) ?? throw new EntityNotFoundException<CodeOutputExercise>(id);

        return this.ToDetailItemWithAnswers(exercise);
    }

    public async Task<CodeOutputExerciseDetailItemWithAnswer> GetByIdWithAnswerAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var exercise = await this.repository.CodeOutputExercises.TryGetById(id, cancellationToken) ?? throw new EntityNotFoundException<CodeOutputExercise>(id);

        return this.ToDetailItemWithoutAnswers(exercise);
    }

    public async Task<CodeOutputExerciseDetailItemWithAnswer> CreateAsync(CodeOutputExerciseCreateItem createItem, Guid userId, CancellationToken cancellationToken = default)
    {
        var nextAvailableExerciseNumberInChapter = (await this.repository.CommonExercises.GetForChapterAsync(createItem.ChapterId, cancellationToken))
            .Select(e => e.RunningNumber)
            .DefaultIfEmpty(0)
            .Max() + 1;
        
        var entity = new CodeOutputExercise
        {
            Id = Guid.NewGuid(),
            ExerciseName = createItem.ExerciseName,
            ChapterId = createItem.ChapterId,
            ExerciseType = ExerciseType.CodeOutput,
            AchievablePoints = createItem.AchieveablePoints,
            Description = createItem.ExerciseDescription,
            IsMultiLineResponse = createItem.IsMultilineResponse,
            ExpectedAnswer = createItem.ExpectedAnswer,
            RunningNumber = nextAvailableExerciseNumberInChapter,
            OwnerId = userId
        };
        var createdEntity = await this.repository.CodeOutputExercises.CreateAsync(entity, cancellationToken);

        return this.ToDetailItemWithAnswers(createdEntity);
    }

    public async Task<CodeOutputExerciseDetailItemWithAnswer> UpdateAsync(CodeOutputExerciseDetailItemWithAnswer item, CancellationToken cancellationToken = default)
    {
        var exercise = await this.repository.CodeOutputExercises.TryGetById(item.Id, cancellationToken) ?? throw new EntityNotFoundException<CodeOutputExercise>(item.Id);
        
        exercise = this.UpdateExercise(exercise, item);
        
        return this.ToDetailItemWithAnswers(await this.repository.CodeOutputExercises.UpdateAsync(exercise, cancellationToken));
    }

    private CodeOutputExercise UpdateExercise(CodeOutputExercise entity, CodeOutputExerciseDetailItemWithAnswer detailItem)
    {
        entity.Id = detailItem.Id;
        entity.ChapterId = detailItem.ChapterId;
        entity.Description = detailItem.ExerciseDescription;
        entity.AchievablePoints = detailItem.AchieveablePoints;
        entity.ExerciseName = detailItem.ExerciseName;
        entity.ExpectedAnswer = detailItem.ExpectedAnswer;
        entity.IsMultiLineResponse = detailItem.IsMultiLineResponse;

        return entity;
    }

    private CodeOutputExerciseDetailItemWithAnswer ToDetailItemWithAnswers(CodeOutputExercise entity)
    {
        return new CodeOutputExerciseDetailItemWithAnswer
        {
            ExpectedAnswer = entity.ExpectedAnswer,
            ExerciseDescription = entity.Description,
            IsMultiLineResponse = entity.IsMultiLineResponse,
            ExerciseType = ExerciseType.CodeOutput,
            Id = entity.Id,
            AchieveablePoints = entity.AchievablePoints,
            ChapterId = entity.ChapterId,
            ExerciseName = entity.ExerciseName,
            CreationDate = entity.CreationTime,
            ModificationDate = entity.ModificationTime,
            RunningNumber = entity.RunningNumber
        };
    }

    private CodeOutputExerciseDetailItemWithAnswer ToDetailItemWithoutAnswers(CodeOutputExercise entity)
    {
        return new CodeOutputExerciseDetailItemWithAnswer
        {
            ExpectedAnswer = entity.ExpectedAnswer,
            ExerciseDescription = entity.Description,
            IsMultiLineResponse = entity.IsMultiLineResponse,
            ExerciseType = ExerciseType.CodeOutput,
            Id = entity.Id,
            AchieveablePoints = entity.AchievablePoints,
            ChapterId = entity.ChapterId,
            ExerciseName = entity.ExerciseName,
            CreationDate = entity.CreationTime,
            ModificationDate = entity.ModificationTime,
            RunningNumber = entity.RunningNumber
        };
    }
}