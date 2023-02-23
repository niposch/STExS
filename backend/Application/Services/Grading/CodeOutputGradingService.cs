using Common.Exceptions;
using Common.Models.ExerciseSystem.CodeOutput;
using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services.Grading;

public class CodeOutputGradingService : ICodeOutputGradingService
{
    private readonly IApplicationRepository repository;

    public CodeOutputGradingService(IApplicationRepository repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task GradeAsync(CodeOutputSubmission submission)
    {
        var exercise = await this.repository.CodeOutputExercises.TryGetById(submission.ExerciseId);
        if (exercise is null)
        {
            throw new EntityNotFoundException<CodeOutputExercise>(submission.ExerciseId);
        }

        var gradingResult = new GradingResult
        {
            Id = Guid.NewGuid(),
            Comment = "Graded automatically",
            IsFinal = false,
            AppealDate = DateTime.Now.AddDays(14),
            IsAutomatic = true,
            CreationDate = DateTime.Now,
            GradedSubmissionId = submission.Id,
            UserId =  submission.UserId,
            ExerciseId = submission.ExerciseId,
            Points = 0
        };

        if(submission.SubmittedAnswer == exercise.ExpectedAnswer)
        {
            gradingResult.Points =  exercise.AchievablePoints;
        }

        await this.repository.GradingResults.CreateAsync(gradingResult);
    }
}

public interface ICodeOutputGradingService
{
    public Task GradeAsync(CodeOutputSubmission submission);
}