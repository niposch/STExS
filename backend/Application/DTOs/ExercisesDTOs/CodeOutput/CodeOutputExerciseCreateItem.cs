namespace Application.DTOs.ExercisesDTOs.CodeOutput;

public class CodeOutputExerciseCreateItem : ExerciseDetailItem
{
    public string ExpectedAnswer { get; set; }
    
    public bool IsMultilineResponse { get; set; }
}