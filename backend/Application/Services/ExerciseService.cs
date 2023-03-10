using Application.DTOs.ExercisesDTOs;
using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.Parson;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services;

public sealed class ExerciseService : IExerciseService
{
    private readonly IApplicationRepository repository;
    private readonly IParsonExerciseService parsonExerciseService;


    public ExerciseService(IApplicationRepository repository, IParsonExerciseService parsonExerciseService)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.parsonExerciseService =
            parsonExerciseService ?? throw new ArgumentException(nameof(parsonExerciseService));
    }

    public async Task<ExerciseDetailItem> CopyToChapterAsync(Guid existingExerciseId, Guid chapterToCopyTo, CancellationToken cancellationToken = default)
    {
        var exercise = await this.repository.CommonExercises.TryGetByIdAsync(existingExerciseId, true, cancellationToken);
        if (exercise is null) throw new ArgumentException($"Exercise with id {existingExerciseId} does not exist");

        var nextAvailableRunningNumber = (await this.repository.CommonExercises.GetForChapterAsync(chapterToCopyTo, cancellationToken))
            .Select(m => m.RunningNumber)
            .DefaultIfEmpty(0)
            .Max() + 1;

        Guid originalId = exercise.Id;
        
        exercise.Id = Guid.NewGuid();

        exercise.ChapterId = chapterToCopyTo;
        exercise.Chapter = null!;
        exercise.RunningNumber = nextAvailableRunningNumber;
        
        exercise = await this.repository.CommonExercises.AddAsync(exercise, cancellationToken);

        if (exercise is ParsonExercise parsonExercise)
            try
            {
                await this.CopySolutionAndElementsAsync(parsonExercise, originalId, cancellationToken);
            }
            catch (Exception e)
            {
                await this.repository.CommonExercises.DeleteAsync(exercise.Id, cancellationToken);
                throw new Exception("Kopieren der Exercise misslungen!");
            }

        return ToDetailItem(exercise, null);
    }

    private async Task CopySolutionAndElementsAsync(ParsonExercise exercise, Guid originalId,
        CancellationToken cancellationToken = default)
    {
        var originalExercise = await this.repository.ParsonExercises.TryGetByIdAsync(originalId, cancellationToken);

        if (originalExercise == null)
        {
            throw new NullReferenceException("Original konnte nicht gefunden werden!");
        }

        var originalSolution = originalExercise.ExpectedSolution;
        var solution = new ParsonSolution
        {
            Id = Guid.NewGuid(),
            RelatedExerciseId = exercise.Id,
            RelatedExercise = exercise,
            OwnerId = exercise.OwnerId,
            IndentationIsRelevant = originalSolution.IndentationIsRelevant
        };
        
        var originalElements = originalSolution.CodeElements;
        var elements = new List<ParsonElement>();

        foreach (var codeElement in originalElements)
        {
            elements.Add(new ParsonElement()
            {
                Id = Guid.NewGuid(),
                OwnerId = codeElement.OwnerId,
                RelatedSolutionId = solution.Id,
                RelatedSolution = solution,
                Code = codeElement.Code,
                Indentation = codeElement.Indentation,
                RunningNumber = codeElement.RunningNumber
            });
        }

        solution.CodeElements = elements;
        
        await this.repository.ParsonSolutions.AddAsync(solution, cancellationToken);
    }
    
    public async Task<List<ExerciseDetailItem>> GetByChapterIdAsync(Guid chapterId, Guid userId, CancellationToken cancellationToken = default)
    {
        var exercises = await this.repository.CommonExercises.GetForChapterAsync(chapterId, cancellationToken);
        var userSubmissions = await this.repository.UserSubmissions.GetAllByUserIdAndChapterAsync(chapterId, userId, cancellationToken);

        return exercises.Select(e =>
                ToDetailItem(e,
                    userSubmissions.Any(s => s.FinalSubmission != null && s.ExerciseId == e.Id)
                ))
            .ToList();
    }

    public async Task DeleteExerciseAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var exercise = await this.repository.CommonExercises.TryGetByIdAsync(exerciseId, true, cancellationToken);

        if (exercise is ParsonExercise)
            await this.DeleteParsonExerciseAsync(exerciseId, cancellationToken);
        else
            await this.repository.CommonExercises.DeleteAsync(exerciseId, cancellationToken);
    }

    private async Task DeleteParsonExerciseAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var submissionCount = this.repository.UserSubmissions.ExistsByExerciseIdAsync(exerciseId, cancellationToken);
        if (0 != submissionCount)
            throw new UnsupportedActionException("Fehler im Löschprozess"); 
        var exercise = await this.repository.ParsonExercises.TryGetByIdAsync(exerciseId, cancellationToken);
        try
        {
            await this.repository.ParsonElements.RemoveRangeAsync(exercise.ExpectedSolution.CodeElements, cancellationToken);
            await this.repository.ParsonSolutions.DeleteAsync(exercise.ExpectedSolution.Id, cancellationToken);
            await this.repository.CommonExercises.DeleteAsync(exerciseId, cancellationToken);
        }
        catch (Exception e)
        {
            throw new Exception("Fehler im Löschprozess");
        }
    }

    public async Task<ExerciseDetailItem> GetExerciseByIdAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var exercise = await this.repository.CommonExercises.TryGetByIdAsync(exerciseId, false, cancellationToken) ?? throw new EntityNotFoundException<BaseExercise>(exerciseId);

        return ToDetailItem(exercise, null);
    }

    public async Task<List<ExerciseDetailItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var all = await this.repository.CommonExercises.GetAllAsync(cancellationToken);

        return all
            .Select(e => ToDetailItem(e, null))
            .ToList();
    }

    public async Task<List<ExerciseDetailItem>> SearchAsync(string search, CancellationToken cancellationToken = default)
    {
        var all = await this.repository.CommonExercises.SearchAsync(search, cancellationToken);

        return all
            .Select(e => ToDetailItem(e, null))
            .ToList();
    }

    private static ExerciseDetailItem ToDetailItem(BaseExercise entity, bool? userHasSolved)
    {
        return new ExerciseDetailItem
        {
            Id = entity.Id,
            ExerciseName = entity.ExerciseName,
            ChapterId = entity.ChapterId,
            AchievablePoints = entity.AchievablePoints,
            CreationDate = entity.CreationTime,
            ExerciseDescription = entity.Description,
            ExerciseType = entity.ExerciseType,
            ModificationDate = entity.ModificationTime,
            RunningNumber = entity.RunningNumber,
            UserHasSolvedExercise = userHasSolved
        };
    }
}