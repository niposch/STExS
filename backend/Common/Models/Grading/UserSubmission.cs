using Common.Models.Authentication;
using Common.Models.ExerciseSystem;

namespace Common.Models.Grading;

public class UserSubmission
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get;set; }
    public Guid ExerciseId { get; set; }
    public BaseExercise Exercise { get; set; }
    
    public Guid? FinalSubmissionId { get; set; }
    public BaseSubmission? FinalSubmission { get; set; }
    
    public List<TimeTrack> TimeTracks { get; set; } = new();
    public List<BaseSubmission> Submissions { get; set; } = new();
    public List<GradingResult> GradingResults { get; set; } = new();
}