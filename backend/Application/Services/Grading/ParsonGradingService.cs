using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.ExerciseSystem.CodeOutput;
using Common.Models.ExerciseSystem.Parson;
using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services.Grading;

public sealed class ParsonGradingService: IParsonGradingService
{
    private readonly IApplicationRepository repository;

    public ParsonGradingService(IApplicationRepository repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task GradeAsync(ParsonPuzzleSubmission submission)
    {
        var exercise = await this.repository.ParsonExercises.TryGetByIdAsync(submission.ExerciseId);
        if (exercise is null)
        {
            throw new EntityNotFoundException<ParsonExercise>(submission.ExerciseId);
        }

        var gradingResult = new GradingResult
        {
            Id = Guid.NewGuid(),
            Comment = "Graded automatically",
            AppealableBefore = DateTime.Now.AddDays(14),
            IsAutomaticallyGraded = true,
            CreationDate = DateTime.Now,
            GradedSubmissionId = submission.Id,
            Points = 0,
            GradingState = GradingState.InProgress
        };
        
        await this.repository.GradingResults.CreateAsync(gradingResult);
        submission.GradingResultId = gradingResult.Id;
        await this.repository.Submissions.UpdateAsync(submission);

        var points = this.CalculateScore(submission, exercise.ExpectedSolution);
        
        gradingResult.GradingState = GradingState.AutomaticallyGraded;
        gradingResult.Points = points;
        
        await this.repository.GradingResults.UpdateAsync(gradingResult);
    }

    private int CalculateScore(ParsonPuzzleSubmission submission, ParsonSolution solution)
    {
        if(solution.CodeElements.Count != solution.RelatedExercise.AchievablePoints)
        {
            return 0;
        }
        submission.AnswerItems = submission.AnswerItems.OrderBy(a => a.RunningNumber).ToList();
        solution.CodeElements = solution.CodeElements.OrderBy(c => c.RunningNumber).ToList();
        int score = 0;
        for(var i = 0; i< submission.AnswerItems.Count; i++)
        {
            var submittedAnswer = submission.AnswerItems[i];
            var correctAnswer = solution.CodeElements[i];
            if ((!solution.IndentationIsRelevant || submittedAnswer.Indentation == correctAnswer.Indentation) && submittedAnswer.ParsonElement.Code == correctAnswer.Code)
            {
                score += 1;
            }
        }

        return score;
    }
}