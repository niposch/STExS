using Application.DTOs;
using Application.Services.Grading;
using Common.Models.Grading;

namespace Application.Services.Interfaces;

public interface IGradingService
{
    public Task<List<ExerciseReportItem>> GetExerciseReportAsync(Guid exerciseId, CancellationToken cancellationToken = default);
    public Task RunAutomaticGradingForExerciseAsync(BaseSubmission submission);

    Task ManuallyGradeExerciseAsync(Guid submissionId,
        int newGrade,
        string comment,
        Guid changedByUserId,
        CancellationToken cancellationToken = default);
}