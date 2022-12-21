using Common.Models.Authentication;
using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem;

public class ModuleParticipation: ICreationTimeTracked
{
    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; } = null!;
    
    public Guid ModuleId { get; set; }
    public virtual Module Module { get; set; } = null!;

    public bool ParticipationConfirmed { get; set; } // a user needs to confirm his participation in a module before he can see it
    
    public DateTime CreationTime { get; set; } // when the user registered for the module
    
    public bool IsFavorite { get; set; } // if the user has marked the module as a favorite`
}