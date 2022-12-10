using System.ComponentModel.DataAnnotations;
using Common.Models.Authentication;
using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem;

public class Chapter : IBaseEntity, IDeletable
{
    [Key]
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }
    public ApplicationUser Owner { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int RunningNumber { get; set; }
    
    public Guid ModuleId { get; set; }
    public Module Module { get; set; } = null!;

    public List<BaseExercise> Exercises { get; set; } = null!; // 1:n Beziehung zu BaseExercise

    // TODO Mahmoud Chapter weitere Felder hinzufügen
}
