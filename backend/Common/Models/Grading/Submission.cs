using Common.Models.ExerciseSystem;
using Common.Models.HelperInterfaces;

namespace Common.Models.Grading;

public abstract class Submission: ICreationTimeTracked
{
    public Guid Id { get; set; }
    public DateTime CreationTime { get; set; }
    
    public UserSubmission UserSubmission { get; set; }
    public Guid UserSubmissionId { get; set; }
    
    public GradingResult GradingResult { get; set; }
    public Guid GradingResultId { get; set; }
    public ExerciseType SubmissionType { get; set; }
}