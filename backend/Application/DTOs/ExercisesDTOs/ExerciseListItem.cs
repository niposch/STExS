using Common.Models.ExerciseSystem;

namespace Application.DTOs.ExercisesDTOs;

public class ExerciseListItem
{
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; }
    public ExerciseType ExerciseType { get; set; }
    public int Order { get; set; }
    public int AchivablePoints { get; set; }
}