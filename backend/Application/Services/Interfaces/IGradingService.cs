using Application.DTOs;
using Application.DTOs.GradingReportDTOs;
using Common.Models.Grading;

namespace Application.Services.Interfaces;

public interface IGradingService
{
    public Task<List<ExerciseReportItem>> GetExerciseReportAsync(Guid exerciseId, CancellationToken cancellationToken = default);
    public Task RunAutomaticGradingForExerciseAsync(BaseSubmission submission);

    public Task<ModuleReport> GetModuleReportAsync(Guid moduleId, CancellationToken cancellationToken = default);
}