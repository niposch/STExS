using Application.Helper;
using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.ExerciseSystem.Cloze;
using Common.Models.ExerciseSystem.CodeOutput;
using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services.Grading;

public sealed class ClozeTextGradingService: IClozeTextGradingService
{
    private readonly IApplicationRepository repository;

    private readonly IClozeTextHelper clozeTextHelper;

    public ClozeTextGradingService(IApplicationRepository repository, IClozeTextHelper clozeTextHelper)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.clozeTextHelper = clozeTextHelper ?? throw new ArgumentNullException(nameof(clozeTextHelper));
    }

    public async Task GradeAsync(ClozeTextSubmission submission)
    {
        var exercise = await this.repository.ClozeTextExercises.TryGetByIdAsync(submission.ExerciseId);
        if (exercise is null)
        {
            throw new EntityNotFoundException<ClozeTextSubmission>(submission.ExerciseId);
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

        var points = this.CalculateScore(submission, exercise);
        gradingResult.GradingState = GradingState.AutomaticallyGraded;
        gradingResult.Points = points;
        
        await this.repository.GradingResults.UpdateAsync(gradingResult);
    }
    
    private int CalculateScore(ClozeTextSubmission submission, ClozeTextExercise exercise)
    {
        var answers = this.clozeTextHelper.GetAnswers(exercise.TextWithAnswers);
        var submittedAnswers = submission.SubmittedAnswers
            .OrderBy(s => s.Index)
            .Select(s => s.SubmittedAnswer)
            .ToList();
        var score = 0;
        for(var i = 0; i < answers.Count; i++)
        {
            if (answers[i] == submittedAnswers[i])
            {
                score += 1;
            }
            if(i >= submittedAnswers.Count)
            {
                break;
            }
        }

        return score;
    }
}