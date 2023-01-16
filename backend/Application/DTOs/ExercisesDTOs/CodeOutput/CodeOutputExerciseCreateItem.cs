namespace Application.DTOs.ExercisesDTOs.CodeOutput;

public class CodeOutputExerciseCreateItem
{
    public string ExpectedAnswer { get; set; }
    
    public bool IsMultilineResponse { get; set; }
    
    public string ExerciseName { get; set; }
    
    public string ExerciseDescription { get; set; }
    
    public int AchieveablePoints { get; set; }
    
    public Guid ChapterId { get; set; }
}