using Common.Models.HelperInterfaces;
using Common.RepositoryInterfaces.Tables;

namespace Common.Models.ExerciseSystem.Parson;
public sealed class ParsonExercise: BaseExercise
{
    // Relationships
    public ParsonSolution ExpectedSolution { get; set; }
    
    public Chapter Chapter { get; set; }
    public bool IndentationIsRelevant { get; set; }
}