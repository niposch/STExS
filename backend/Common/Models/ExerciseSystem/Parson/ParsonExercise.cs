using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.Models.ExerciseSystem.Parson;

public sealed class ParsonExercise : BaseExercise
{
    // Relationships
    public ParsonSolution ExpectedSolution { get; set; }
}
