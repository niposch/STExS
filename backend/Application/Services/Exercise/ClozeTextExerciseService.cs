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

    public ClozeTextExerciseService(IApplicationRepository repository, IClozeTextHelper clozeTextHelper)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.clozeTextHelper = clozeTextHelper ?? throw new ArgumentNullException(nameof(clozeTextHelper));
    }

    public async Task<ClozeTextExerciseDetailItem> GetByIdAsync(Guid exerciseId,
        Guid userId,
        bool withAnswers,
        CancellationToken cancellationToken = default)
    {
        var exercise = await this.repository.ClozeTextExercises.TryGetByIdAsync(exerciseId, cancellationToken);
        if (exercise == null) throw new EntityNotFoundException<ClozeTextExercise>(exerciseId);
        var userHasSolvedExercise =
            var res = new ClozeTextExerciseDetailItem(exercise, withAnswers,);
        return res;
    }

    public async Task UpdateAsync(ClozeTextExerciseDetailItem updateItem, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> CreateAsync(ClozeTextExerciseCreateItem createItem, Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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