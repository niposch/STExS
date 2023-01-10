using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Models.HelperInterfaces;
using Common.Models.Authentication;

namespace Common.Models.ExerciseSystem;

public abstract class BaseExercise : DeletableBaseEntity,
    ICreationTimeTracked,
    IModificationTimeTracked
{
    public DateTime? ModificationTime { get; set; }

    public DateTime CreationTime { get; set; }

    public Chapter Chapter { get; set; } = null!;
    public Guid ChapterId { get; set; }


    public string Title { get; set; } = null!;
    
    [Column(TypeName = "nvarchar(max)")]
    public string? Description { get; set; }

    public int RunningNumber { get; set; } // used for ordering in related Chapter

    public int AchieveablePoints { get; set; } // how many points are possible in this exercise

    public ExerciseType ExerciseType { get; set; }
}