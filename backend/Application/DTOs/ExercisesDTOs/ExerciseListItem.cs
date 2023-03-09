using Common.Models.ExerciseSystem;

namespace Application.DTOs.ExercisesDTOs;

public class ExerciseListItem
{
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; }
    public ExerciseType ExerciseType { get; set; }
    public int Order { get; set; }
    public int AchivablePoints { get; set; }

    public int RunningNumber { get; internal set; }

    public static ExerciseListItem ToListItem(BaseExercise exercise)
    {
        return new ExerciseListItem
        {
            Order = exercise.RunningNumber,
            ExerciseType = exercise.ExerciseType,
            ExerciseName = exercise.ExerciseName,
            AchivablePoints = exercise.AchievablePoints,
            ExerciseId = exercise.Id,
            RunningNumber = exercise.RunningNumber
        };
    }
}