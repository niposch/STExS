using Application.DTOs;

namespace Application.Services.Interfaces;

public interface IGradingService
{
    public Task<List<ExerciseReportItem>> GetExerciseReportAsync(Guid exerciseId, CancellationToken cancellationToken = default);
}