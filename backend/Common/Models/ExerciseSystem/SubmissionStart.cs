using Common.Models.Authentication;

namespace Common.Models.ExerciseSystem;

public class SubmissionStart
{
    Guid Id { get; set; }
    
    Guid ExerciseId { get; set; }
    BaseExercise Exercise { get; set; }
    
    Guid UserId { get; set; }
    ApplicationUser User { get; set; }
    
    public DateTime StartDate { get; set; }
}