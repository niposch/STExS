using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem.Parson;
public sealed class ParsonExercise: BaseExercise, IDeletable
{
    public DateTime? DeletedDate { get; set; }
    
    // Relationships
    public ParsonSolution ExpectedSolution { get; set; }
}