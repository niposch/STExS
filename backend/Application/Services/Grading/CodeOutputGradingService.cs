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
            FinalAppealDate = DateTime.Now.AddDays(14),
            IsAutomaticallyGraded = true,
            CreationDate = DateTime.Now,
            GradedSubmissionId = submission.Id,
            Points = 0,
            GradingState = GradingState.InProgress
        };
        
        await this.repository.GradingResults.CreateAsync(gradingResult);
        submission.GradingResultId = gradingResult.Id;
        await this.repository.Submissions.UpdateAsync(submission);

        if(submission.SubmittedAnswer == exercise.ExpectedAnswer)
        {
            gradingResult.Points =  exercise.AchievablePoints;
        }
        
        gradingResult.GradingState = GradingState.Graded;
        
        await this.repository.GradingResults.UpdateAsync(gradingResult);
    }
}

public interface ICodeOutputGradingService
{
    public Task GradeAsync(CodeOutputSubmission submission);
}