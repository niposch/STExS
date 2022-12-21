using System.ComponentModel.DataAnnotations;
using Common.Models.Authentication;
using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem;

public class Module : IBaseEntity, IArchiveable
{
    [Key]
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }

    public ApplicationUser Owner { get; set; } = null!;

    public DateTime? ArchivedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public List<Chapter> Chapters { get; set; } = null!; // 1:n Beziehung zu Chapters

    public string ModuleName { get; set; } = null!;
    public string ModuleDescription { get; set; } = null!;
    public List<ModuleParticipation> ModuleParticipations { get; set; } = new();
}
