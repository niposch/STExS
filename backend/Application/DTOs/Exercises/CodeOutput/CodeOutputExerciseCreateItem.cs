namespace Application.DTOs.Exercises.CodeOutput;

public class CodeOutputExerciseCreateItem : ExerciseDetailItem
{
    public string ExpectedAnswer { get; set; }
    
    public bool IsMultilineResponse { get; set; }
}