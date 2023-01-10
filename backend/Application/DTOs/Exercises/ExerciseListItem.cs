using Common.Models.ExerciseSystem;

namespace Application.DTOs.Exercises;

public class ExerciseListItem
{
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; }
    public ExerciseType ExerciseType { get; set; }
    public int Order { get; set; }
    public int ReachableScore { get; set; }
}