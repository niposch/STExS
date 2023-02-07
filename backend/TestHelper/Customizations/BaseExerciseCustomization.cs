using System.Collections;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.CodeOutput;
using Common.Models.Grading;

namespace TestHelper.Customizations;

public class BaseExerciseCustomization: ICustomization
{
    // list of BaseExercise should be created as a list of CodeOutputExercise, ParsonPuzzleExercise
    // or any other derived class
    public void Customize(IFixture fixture)
    {
        fixture.Customizations.Add(new TypeRelay(typeof(BaseExercise), typeof(CodeOutputExercise)));
        fixture.Customizations.Add(new TypeRelay(typeof(BaseSubmission), typeof(CodeOutputSubmission)));
    }
}