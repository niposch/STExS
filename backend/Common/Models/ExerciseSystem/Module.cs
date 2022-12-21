using System.ComponentModel.DataAnnotations;
using Common.Models.Authentication;
using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem;

public class Module :ArchiveableBaseEntity
{
    public List<Chapter> Chapters { get; set; } = null!; // 1:n Beziehung zu Chapters

    public string ModuleName { get; set; } = null!;
    public string ModuleDescription { get; set; } = null!;
    public List<ModuleParticipation> ModuleParticipations { get; set; } = new();
}
