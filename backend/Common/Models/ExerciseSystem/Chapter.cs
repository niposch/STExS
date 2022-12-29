using System.ComponentModel.DataAnnotations;
using Common.Models.Authentication;
using Common.Models.ExerciseSystem.Parson;
using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem;

public class Chapter : DeletableBaseEntity, ICreationTimeTracked
{
    public int RunningNumber { get; set; }
    
    public string ChapterName { get; set; } = string.Empty;
    
    public string ChapterDescription { get; set; } = string.Empty;
    
    public Guid ModuleId { get; set; }
    public Module Module { get; set; } = null!;

    public List<ParsonExercise> ParsonExercises { get; set; } = null!; // 1:n Beziehung zu ParsonExercise
    
    public DateTime CreationTime { get; set; }
}