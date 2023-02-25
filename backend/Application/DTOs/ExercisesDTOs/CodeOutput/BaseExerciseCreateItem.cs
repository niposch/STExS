namespace Application.DTOs.ExercisesDTOs.CodeOutput;

public class BaseExerciseCreateItem
{
    public string ExpectedAnswer { get; set; }
    
    public string ExerciseName { get; set; }
    
    public string ExerciseDescription { get; set; }
    
    public Guid ChapterId { get; set; }
}