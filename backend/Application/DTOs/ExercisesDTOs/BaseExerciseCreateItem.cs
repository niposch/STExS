namespace Application.DTOs.ExercisesDTOs;

public class BaseExerciseCreateItem
{
    public string ExerciseName { get; set; }

    public string ExerciseDescription { get; set; }

    public Guid ChapterId { get; set; }

    public int AchievablePoints { get; set; }
}