using Common.Models.HelperInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.ExerciseSystem;

public abstract class BaseExercise : IBaseEntity,
    ICreationTimeTracked,
    IModificationTimeTracked
{
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }

    public DateTime? ModificationTime { get; set; }

    public DateTime CreationTime { get; set; }


    public string? Name { get; set; }

    public int RunningNumber { get; set; }

    public int AchieveablePoints { get; set; }
}
