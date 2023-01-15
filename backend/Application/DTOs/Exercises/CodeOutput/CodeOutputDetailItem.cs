using Common.Models.ExerciseSystem.CodeOutput;

namespace Application.DTOs.Exercises.CodeOutput;

public class CodeOutputDetailItem: ExerciseDetailItem
{
    public bool IsMultiLineResponse { get; set; }
}