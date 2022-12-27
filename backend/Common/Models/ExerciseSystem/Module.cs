using System.ComponentModel.DataAnnotations;
using Common.Models.Authentication;
using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem;

public class Module :ArchiveableBaseEntity, ICreationTimeTracked
{
    public List<Chapter> Chapters { get; set; } = null!; // 1:n Beziehung zu Chapters

    public string ModuleName { get; set; } = null!;
    public string ModuleDescription { get; set; } = null!;
    public virtual ICollection<ModuleParticipation> ModuleParticipations { get; set; }
    
    public DateTime CreationTime { get; set; }
}
