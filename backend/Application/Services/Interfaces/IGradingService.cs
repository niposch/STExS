using Application.DTOs;
using Common.Models.Grading;

namespace Application.Services.Interfaces;

public interface IGradingService
{
    public Task<List<ExerciseReportItem>> GetExerciseReportAsync(Guid exerciseId, CancellationToken cancellationToken = default);
    public Task RunAutomaticGradingForExerciseAsync(BaseSubmission submission);
}