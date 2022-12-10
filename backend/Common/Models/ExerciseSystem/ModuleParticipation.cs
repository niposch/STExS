using Common.Models.Authentication;
using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem;

public sealed class ModuleParticipation: ICreationTimeTracked
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    
    public Guid ModuleId { get; set; }
    public Module Module { get; set; }

    public bool ParticipationConfirmed { get; set; } // a user needs to confirm his participation in a module before he can see it
    
    public DateTime CreationTime { get; set; } // when the user registered for the module
}