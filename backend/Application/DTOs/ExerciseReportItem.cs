using Common.Models.ExerciseSystem;

namespace Application.DTOs;

public sealed class ExerciseReportItem
{
    public Guid ExerciseId { get; set; }
    public ExerciseType ExerciseType { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public string UserFirstName { get; set; } = string.Empty;
    public string UserLastName { get; set; } = string.Empty;
    public string MatrikelNumber { get; set; } = string.Empty;
    public Guid? LastSubmissionId { get; set; }
    public DateTime? LastSubmissionTime { get; set; }
    public DateTime? LastGradingTime { get; set; }
    public int? LastPoints { get; set; }
    public string? LastComment { get; set; } = string.Empty;
    public bool? LastIsFinalSubmission { get; set; }
    public bool? LastIsAutomatic { get; set; }
    public SubmissionState LastSubmissionState { get; set; }
    public SubmissionGradingState LastSubmissionGradingState { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public bool IsFinalSubmission { get; set; }
}

public enum SubmissionState
{
    NotStarted,
    StartedButNothingSubmitted,
    TemporarySubmitted,
    FinalSolutionSubmitted
}

public enum SubmissionGradingState
{
    NotGraded,
    AutomaticGraded,
    ManuallyGraded
}
