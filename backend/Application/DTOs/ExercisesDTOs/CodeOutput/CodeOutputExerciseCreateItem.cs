﻿namespace Application.DTOs.ExercisesDTOs.CodeOutput;

public class CodeOutputExerciseCreateItem : BaseExerciseCreateItem
{
    public bool IsMultilineResponse { get; set; }

    public string ExpectedAnswer { get; set; }
}