using System.ComponentModel.DataAnnotations;
using Common.Models.Authentication;
using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem.Parson;

public sealed class ParsonSolution: IBaseEntity, IDeletable, ICreationTimeTracked, IModificationTimeTracked
{
    [Key]
    public Guid Id { get; set; }
    
    public DateTime? DeletedDate { get; set; }
    
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    
    // Relationships
    public List<ParsonElement> CodeElements { get; set; } = null!;
    
    public ParsonExercise RelatedExercise { get; set; } = null!;
    public Guid RelatedExerciseId { get; set; }
    
    public ApplicationUser Owner { get; set; } = null!;
    public Guid OwnerId { get; set; }
}