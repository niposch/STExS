using System.ComponentModel.DataAnnotations;
using Common.Models.Authentication;
using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem.Parson;

public sealed class ParsonElement: IBaseEntity, IDeletable, ICreationTimeTracked, IModificationTimeTracked
{
    [Key]
    public Guid Id { get; set; }
    
    public Guid OwnerId { get; set; }
    public ApplicationUser Owner { get; set; } = null!;
    
    public DateTime? DeletedDate { get; set; }
    
    public string Code { get; set; } = string.Empty;
    
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    
    // Relationships
    public ParsonSolution RelatedSolution { get; set; } = null!;
    public Guid RelatedSolutionId { get; set; }
}