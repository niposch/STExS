using Common.Models.Authentication;

namespace Common.Models.Grading;

/// <summary>
/// when a user starts to work on a submission, a new TimeTrack is created
/// when the user is done with their work, the TimeTrack is closed
/// the last updated time is updated every time the user saves their work
/// timetracks are supposed to last only a few minutes to a few hours
/// if the user does not close the timetrack within a day of creation, it is assumed that the user has stopped working on the submission
/// the last updated time is then used as the closed time
/// if both the closed time and the last updated time are null and the timetrack is older than a day, it is assumed that the user has stopped working on the submission
/// and hasn't saved their work
/// </summary>
public class TimeTrack
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime? CloseDateTime { get; set; }
    public DateTime? LastUpdate { get; set; }
    
    public ApplicationUser User { get; set; }
    public Guid UserId { get; set; }
    public Guid ExerciseId { get; set; }
    
    public UserSubmission UserSubmission { get; set; }
}