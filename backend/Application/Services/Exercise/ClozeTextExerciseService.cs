using Application.DTOs.ExercisesDTOs.ClozeText;
using Application.Helper;
using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.Cloze;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services.Exercise;

public sealed class ClozeTextExerciseService : IClozeTextExerciseService
{
    private readonly IClozeTextHelper clozeTextHelper;
    private readonly IApplicationRepository repository;
    private readonly IUserSubmissionService submissionService;

    public ClozeTextExerciseService(IApplicationRepository repository,
        IClozeTextHelper clozeTextHelper,
        IUserSubmissionService submissionService)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.clozeTextHelper = clozeTextHelper ?? throw new ArgumentNullException(nameof(clozeTextHelper));
        this.submissionService = submissionService ?? throw new ArgumentNullException(nameof(submissionService));
    }

    public async Task<ClozeTextExerciseDetailItem> GetByIdAsync(Guid exerciseId,
        Guid userId,
        bool withAnswers,
        CancellationToken cancellationToken = default)
    {
        var exercise = await this.repository.ClozeTextExercises.TryGetByIdAsync(exerciseId, cancellationToken);
        if (exercise == null) throw new EntityNotFoundException<ClozeTextExercise>(exerciseId);
        var userHasSolvedExercise = await this.submissionService.HasUserSolvedExerciseAsync(userId, exerciseId, cancellationToken);
            var res = this.ToDetailItem(exercise, withAnswers, userHasSolvedExercise);
        return res;
    }

    public async Task UpdateAsync(ClozeTextExerciseDetailItem updateItem, CancellationToken cancellationToken = default)
    {
        var exercise = await this.repository.ClozeTextExercises.TryGetByIdAsync(updateItem.Id, cancellationToken);
        if (exercise == null) throw new EntityNotFoundException<ClozeTextExercise>(updateItem.Id);

        exercise.Description = updateItem.ExerciseDescription;
        exercise.AchievablePoints = updateItem.AchievablePoints;
        exercise.TextWithAnswers = updateItem.Text;
        exercise.ExerciseName = updateItem.ExerciseName;

        await this.repository.ClozeTextExercises.UpdateAsync(exercise, cancellationToken);
    }

    public async Task<Guid> CreateAsync(ClozeTextExerciseCreateItem createItem, Guid userId, CancellationToken cancellationToken = default)
    {
        var nextAvailableExerciseNumberInChapter = (await this.repository.CommonExercises.GetForChapterAsync(createItem.ChapterId, cancellationToken))
            .Select(e => e.RunningNumber)
            .DefaultIfEmpty(0)
            .Max() + 1;

        var entity = new ClozeTextExercise
        {
            Id = Guid.NewGuid(),
            ExerciseName = createItem.ExerciseName,
            ChapterId = createItem.ChapterId,
            ExerciseType = ExerciseType.ClozeText,
            AchievablePoints = createItem.AchievablePoints,
            Description = createItem.ExerciseDescription,
            TextWithAnswers = createItem.Text,
            RunningNumber = nextAvailableExerciseNumberInChapter,
            OwnerId = userId
        };
        var createdEntity = await this.repository.ClozeTextExercises.AddAsync(entity, cancellationToken);
        return createdEntity.Id;
    }

    private ClozeTextExerciseDetailItem ToDetailItem(ClozeTextExercise exercise, bool withAnswers, bool userHasSolvedExercise)
    {
        return new ClozeTextExerciseDetailItem
        {
            ExerciseDescription = exercise.Description,
            AchievablePoints = exercise.AchievablePoints,
            ChapterId = exercise.ChapterId,
            Id = exercise.Id,
            Text = withAnswers ? exercise.TextWithAnswers : this.clozeTextHelper.StripAnswers(exercise.TextWithAnswers),
            CreationDate = exercise.CreationTime,
            ExerciseName = exercise.ExerciseName,
            ExerciseType = ExerciseType.ClozeText,
            ModificationDate = exercise.ModificationTime,
            RunningNumber = exercise.RunningNumber,
            UserHasSolvedExercise = userHasSolvedExercise
        };
    }
}