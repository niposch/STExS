using System.ComponentModel.DataAnnotations;
using Common.Models.Authentication;
using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem.Parson;

public sealed class ParsonSolution: DeletableBaseEntity, ICreationTimeTracked, IModificationTimeTracked
{
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    
    // Relationships
    public List<ParsonElement> CodeElements { get; set; } = null!;
    
    public ParsonExercise RelatedExercise { get; set; } = null!;
    public Guid RelatedExerciseId { get; set; }
    
    public bool IndentationIsRelevant { get; set; } = false;
}