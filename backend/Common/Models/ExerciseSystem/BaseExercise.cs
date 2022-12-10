using System.ComponentModel.DataAnnotations;
using Common.Models.HelperInterfaces;
using Common.Models.Authentication;

namespace Common.Models.ExerciseSystem;

public abstract class BaseExercise : IBaseEntity,
    ICreationTimeTracked,
    IModificationTimeTracked
{
    [Key]
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }
    public ApplicationUser Owner { get; set; }

    public DateTime? ModificationTime { get; set; }

    public DateTime CreationTime { get; set; }
    
    public Chapter Chapter { get; set; }
    public Guid ChapterId { get; set; }


    public string Title { get; set; }
    
    public string? Description { get; set; }

    public int RunningNumber { get; set; } // used for ordering in related Chapter

    public int AchieveablePoints { get; set; }
}
