using Application.DTOs;
using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.CodeOutput;
using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services.Grading;

public sealed class GradingService : IGradingService
{
    private readonly IApplicationRepository applicationRepository;

    private readonly ICodeOutputGradingService codeOutputGradingService;

    public GradingService(IApplicationRepository applicationRepository, ICodeOutputGradingService codeOutputGradingService)
    {
        this.applicationRepository = applicationRepository;
        this.codeOutputGradingService = codeOutputGradingService ?? throw new ArgumentNullException(nameof(codeOutputGradingService));
    }

    public async Task RunAutomaticGradingForExerciseAsync(BaseSubmission submission)
    {
        if(submission is CodeOutputSubmission codeOutputSubmission)
        {
            await this.codeOutputGradingService.GradeAsync(codeOutputSubmission);
        }
    }

    public async Task<List<ExerciseReportItem>> GetExerciseReportAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var exercise = await this.applicationRepository.CommonExercises.TryGetByIdAsync(exerciseId, false, cancellationToken);

        if (exercise == null)
        {
            throw new EntityNotFoundException<BaseExercise>(exerciseId);
        }
        
        var chapter = await this.applicationRepository.Chapters.TryGetByIdAsync(exercise.ChapterId, cancellationToken);
        if (chapter == null)
        {
            throw new EntityNotFoundException<Chapter>(exercise.ChapterId);
        }

        var reportItems = new List<ExerciseReportItem>();

        var moduleParticipations = await this.applicationRepository.ModuleParticipations.GetParticipationsForModuleAsync(chapter.ModuleId, cancellationToken);
        var allSubmissions = await this.applicationRepository.UserSubmissions.GetAllByExerciseIdGroupedByUserIdAsync(exerciseId, cancellationToken);

        foreach (var participation in moduleParticipations)
        {
            allSubmissions.TryGetValue(participation.UserId, out var userSubmission);
            BaseSubmission? lastSubmission = null;
            var lastSubmissionState = SubmissionState.NotStarted;
            var isFinalSubmission = false;
            if (userSubmission != null)
            {
                if (userSubmission.FinalSubmission == null)
                {
                    lastSubmission = userSubmission.Submissions
                                     .OrderByDescending(s => s.CreationTime).FirstOrDefault();
                    
                    isFinalSubmission = false;
                    lastSubmissionState = lastSubmission == null ? SubmissionState.StartedButNothingSubmitted : SubmissionState.TemporarySubmitted;
                }
                else
                {
                    isFinalSubmission = true;
                    lastSubmission = userSubmission.FinalSubmission;
                    lastSubmissionState = SubmissionState.FinalSolutionSubmitted;
                }
            }
            var lastSubmissionGradingState = SubmissionGradingState.NotGraded;
            if(lastSubmission?.GradingResult != null)
            {
                lastSubmissionGradingState = lastSubmission.GradingResult.IsAutomaticallyGraded ? SubmissionGradingState.AutomaticGraded : SubmissionGradingState.ManuallyGraded;
            }

            var item = new ExerciseReportItem
            {
                ExerciseName = exercise.ExerciseName,
                ExerciseId = exerciseId,
                UserId = participation.UserId,
                UserName = participation.User.UserName,
                UserFirstName = participation.User.FirstName,
                UserEmail = participation.User.Email,
                UserLastName = participation.User.LastName,
                ExerciseType = exercise.ExerciseType,
                MatrikelNumber = participation.User.MatrikelNumber,
                LastIsFinalSubmission = userSubmission == null ? null : userSubmission.FinalSubmission != null,
                LastComment = userSubmission?.FinalSubmission?.GradingResult?.Comment,
                LastPoints = lastSubmission?.GradingResult?.Points,
                LastGradingTime = lastSubmission?.GradingResult?.CreationDate,
                LastIsAutomatic = lastSubmission?.GradingResult?.IsAutomaticallyGraded,
                LastSubmissionId = lastSubmission?.Id,
                LastSubmissionTime = lastSubmission?.CreationTime,
                IsFinalSubmission = isFinalSubmission,
                LastSubmissionState = lastSubmissionState,
                LastSubmissionGradingState = lastSubmissionGradingState
            };
            
            reportItems.Add(item);
        }
        
        return reportItems;
    }
}
