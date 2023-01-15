using System.ComponentModel.DataAnnotations.Schema;
using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem.CodeOutput;

public class CodeOutputExercise: BaseExercise
{
    [Column(TypeName = "nvarchar(max)")]
    public string ExpectedAnswer { get; set; }
    
    public bool IsMultiLineResponse { get; set; }
}