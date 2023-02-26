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

    public async Task UpdateAsync(ParsonExerciseDetailItemWithAnswer updateItem, CancellationToken cancellationToken = default)
    {
        var exercise = await this.repository.ParsonExercises.TryGetByIdAsync(updateItem.Id, cancellationToken) ??
                       throw new EntityNotFoundException<ParsonExercise>(updateItem.Id);

        var linesToDelete = exercise.ExpectedSolution.CodeElements.Where(e => !updateItem.Lines.Any(l => l.Id == e.Id)).ToList();

        await this.repository.ParsonElements.RemoveRangeAsync(linesToDelete, cancellationToken);
        exercise.ExpectedSolution.CodeElements.RemoveAll(e => linesToDelete.Select(l => l.Id).Contains(e.Id));

        // remove lines with id's that are not in the existing entity => only Guid.Empty lines are added
        var newLines = new List<ParsonElement>();
        for (var i = 0; i < updateItem.Lines.Count; i++)
        {
            var line = updateItem.Lines[i];
            if (line.Id != Guid.Empty)
            {
                var element = exercise.ExpectedSolution.CodeElements.FirstOrDefault(e => e.Id == line.Id);
                if (element != null)
                {
                    element.Code = line.Text;
                    element.Indentation = line.Indentation;
                    element.RunningNumber = i + 1;
                    newLines.Add(element);
                    await this.repository.ParsonElements.UpdateAsync(element, cancellationToken);
                }
            }
            else
            {
                newLines.Add(new ParsonElement
                {
                    Id = Guid.NewGuid(),
                    Code = line.Text,
                    Indentation = line.Indentation,
                    RunningNumber = i + 1,
                    OwnerId = exercise.OwnerId,
                    RelatedSolutionId = exercise.ExpectedSolution.Id
                });

                await this.repository.ParsonElements.AddAsync(newLines.Last(), cancellationToken);
            }
        }

        exercise = this.UpdateExercise(exercise, updateItem);
        exercise.ExpectedSolution.CodeElements = newLines;
        await this.repository.ParsonExercises.UpdateAsync(exercise, cancellationToken);
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

        createItem.Lines ??= new List<ParsonExerciseLineCreateItem>();
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

        return createdEntity.Id;
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
            Lines = entity.ExpectedSolution
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
            Lines = entity.ExpectedSolution
                .CodeElements
                .Select(l => new ParsonExerciseLineDetailItem
                {
                    Id = l.Id,
                    Indentation = l.Indentation,
                    Text = l.Code
                }).ToList()
        };
    }

    // doesn't update the lines
    private ParsonExercise UpdateExercise(ParsonExercise entity, ParsonExerciseDetailItemWithAnswer detailItem)
    {
        entity.ChapterId = detailItem.ChapterId;
        entity.Description = detailItem.ExerciseDescription;
        entity.AchievablePoints = detailItem.AchieveablePoints;
        entity.ExerciseName = detailItem.ExerciseName;
        return entity;
    }
}