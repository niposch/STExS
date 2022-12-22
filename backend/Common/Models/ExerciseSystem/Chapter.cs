using System.ComponentModel.DataAnnotations;
using Common.Models.Authentication;
using Common.Models.ExerciseSystem.Parson;
using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem;

public class Chapter : DeletableBaseEntity
{
    public int RunningNumber { get; set; }
    
    public Guid ModuleId { get; set; }
    public Module Module { get; set; } = null!;

    public List<ParsonExercise> ParsonExercises { get; set; } = null!; // 1:n Beziehung zu ParsonExercise

    // TODO Mahmoud Chapter weitere Felder hinzufügen
}
