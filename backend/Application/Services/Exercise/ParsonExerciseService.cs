using Application.DTOs.ExercisesDTOs.Parson;
using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.CodeOutput;
using Common.Models.ExerciseSystem.Parson;
using Common.RepositoryInterfaces.Generic;
using Common.RepositoryInterfaces.Tables;

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
                .Select(l => new ParsonExerciseLineDetailItem()
                {
                    Id = l.Id,
                    Indentation = 0,
                    Text = l.Code
                }).ToList()
        };
    }

    public async Task<ParsonExerciseDetailItemWithAnswer> GetByIdWithAnswerAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var exercise = await this.repository.ParsonExercises.TryGetByIdAsync(exerciseId, cancellationToken) ?? throw new EntityNotFoundException<ParsonExercise>(exerciseId);

        return this.ToDetailItemWithAnswers(exercise);
    }

    private ParsonExerciseDetailItemWithAnswer ToDetailItemWithAnswers(ParsonExercise entity,  bool userHasSolvedExercise)
    {
        return new ParsonExerciseDetailItemWithAnswer()
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
                .Select(l => new ParsonExerciseLineDetailItem()
                {
                    Id = l.Id,
                    Indentation = l.,
                    Text = l.Code
                }).ToList()
        };
    }

    public Task<ParsonExerciseDetailItemWithAnswer> UpdateAsync(ParsonExerciseDetailItemWithAnswer item, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ParsonExerciseDetailItemWithAnswer> CreateAsync(ParsonExerciseCreateItem createItem, Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}