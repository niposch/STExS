using System.ComponentModel.DataAnnotations;
using Common.Models.Authentication;
using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem.Parson;

public sealed class ParsonElement: DeletableBaseEntity, ICreationTimeTracked, IModificationTimeTracked
{
    public string Code { get; set; } = string.Empty;

    public int Indentation { get; set; } = 0;

    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    
    // Relationships
    public ParsonSolution RelatedSolution { get; set; } = null!;
    public Guid RelatedSolutionId { get; set; }
}