using Application.DTOs.ExercisesDTOs.Parson;
using Application.Services.Interfaces;
using Common.Exceptions;
using Common.ExtensionMethods;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.Parson;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services.Exercise;

public class ParsonExerciseService : IParsonExerciseService
{
    private readonly IApplicationRepository repository;

    private readonly IUserSubmissionService submissionService;


    public ParsonExerciseService(IApplicationRepository repository, IUserSubmissionService submissionService)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.submissionService = submissionService ?? throw new ArgumentNullException(nameof(submissionService));
    }

    public async Task<ParsonDetailItem> GetByIdAsync(Guid exerciseId, Guid userId, CancellationToken cancellationToken = default)
    {
        var exercise = await this.repository.ParsonExercises.TryGetByIdAsync(exerciseId, cancellationToken) ?? throw new EntityNotFoundException<ParsonExercise>(exerciseId);

        var hasUserSolvedExercise = await this.submissionService.GetOrCreateUserSubmissionAsync(userId, exerciseId, cancellationToken);
        return this.ToDetailItemWithoutAnswers(exercise, hasUserSolvedExercise.FinalSubmissionId != null);
    }

    public async Task<ParsonExerciseDetailItemWithAnswer> GetByIdWithAnswerAsync(Guid exerciseId, Guid userId, CancellationToken cancellationToken = default)
    {
        var exercise = await this.repository.ParsonExercises.TryGetByIdAsync(exerciseId, cancellationToken) ?? throw new EntityNotFoundException<ParsonExercise>(exerciseId);

        var hasUserSolvedExercise = await this.submissionService.GetOrCreateUserSubmissionAsync(userId, exerciseId, cancellationToken);

        return this.ToDetailItemWithAnswers(exercise, hasUserSolvedExercise.FinalSubmissionId != null);
    }

    public async Task UpdateAsync(ParsonExerciseDetailItemWithAnswer item, CancellationToken cancellationToken = default)
    {
        var exercise = await this.repository.ParsonExercises.TryGetByIdAsync(item.Id, cancellationToken) ??
                       throw new EntityNotFoundException<ParsonExercise>(item.Id);

        exercise = this.UpdateExercise(exercise, item);
        ;
    }

    public async Task<Guid> CreateAsync(ParsonExerciseCreateItem createItem, Guid userId, CancellationToken cancellationToken = default)
    {
        var nextAvailableExerciseNumberInChapter = (await this.repository.CommonExercises.GetForChapterAsync(createItem.ChapterId, cancellationToken))
            .Select(e => e.RunningNumber)
            .DefaultIfEmpty(0)
            .Max() + 1;

        var entity = new ParsonExercise
        {
            Id = Guid.NewGuid(),
            ExerciseName = createItem.ExerciseName,
            ChapterId = createItem.ChapterId,
            ExerciseType = ExerciseType.CodeOutput,
            AchievablePoints = createItem.AchieveablePoints,
            Description = createItem.ExerciseDescription,
            ExpectedSolution = new ParsonSolution(),
            RunningNumber = nextAvailableExerciseNumberInChapter,
            OwnerId = userId
        };

        entity.ExpectedSolution.CodeElements = createItem.Lines.Select((l, i) => new ParsonElement
        {
            Id = Guid.NewGuid(),
            Code = l.Text,
            Indentation = l.Indentation,
            RunningNumber = i + 1,
            OwnerId = userId,
            RelatedSolutionId = entity.Id
        }).ToList();

        var createdEntity = await this.repository.ParsonExercises.AddAsync(entity, cancellationToken);

        return entity.Id;
    }

    private ParsonDetailItem ToDetailItemWithoutAnswers(ParsonExercise entity, bool userHasSolvedExercise)
    {
        return new ParsonDetailItem
        {
            ExerciseDescription = entity.Description,
            ExerciseType = ExerciseType.Parson,
            Id = entity.Id,
            AchieveablePoints = entity.AchievablePoints,
            ChapterId = entity.ChapterId,
            ExerciseName = entity.ExerciseName,
            CreationDate = entity.CreationTime,
            ModificationDate = entity.ModificationTime,
            RunningNumber = entity.RunningNumber,
            UserHasSolvedExercise = userHasSolvedExercise,
            ParsonLineList = entity.ExpectedSolution
                .CodeElements
                .Select(l => new ParsonExerciseLineDetailItem
                {
                    Id = l.Id,
                    Indentation = 0,
                    Text = l.Code
                }).Randomize()
                .ToList()
        };
    }

    private ParsonExerciseDetailItemWithAnswer ToDetailItemWithAnswers(ParsonExercise entity, bool userHasSolvedExercise)
    {
        return new ParsonExerciseDetailItemWithAnswer
        {
            ExerciseDescription = entity.Description,
            ExerciseType = ExerciseType.Parson,
            Id = entity.Id,
            AchieveablePoints = entity.AchievablePoints,
            ChapterId = entity.ChapterId,
            ExerciseName = entity.ExerciseName,
            CreationDate = entity.CreationTime,
            ModificationDate = entity.ModificationTime,
            RunningNumber = entity.RunningNumber,
            UserHasSolvedExercise = userHasSolvedExercise,
            ExpectedAnswer = entity.ExpectedSolution
                .CodeElements
                .Select(l => new ParsonExerciseLineDetailItem
                {
                    Id = l.Id,
                    Indentation = l.Indentation,
                    Text = l.Code
                }).ToList()
        };
    }

    private ParsonExercise UpdateExercise(ParsonExercise entity, ParsonExerciseDetailItemWithAnswer detailItem)
    {
        entity.ChapterId = detailItem.ChapterId;
        entity.Description = detailItem.ExerciseDescription;
        entity.AchievablePoints = detailItem.AchieveablePoints;
        entity.ExerciseName = detailItem.ExerciseName;
        // TODO update entity.ExpectedSolution  : overwrite old list with new list transformed from DetailsANsweretc.
        return entity;
    }
}