using Application.DTOs;
using Application.DTOs.GradingReportDTOs;
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
        
    public Task<ModuleReport> GetModuleReportAsync(Guid moduleId, CancellationToken cancellationToken = default);
    
    public Task<GradingResult?> GetGradingResultForSubmissionAsync(Guid submissionId, CancellationToken cancellationToken);
}