using System.ComponentModel.DataAnnotations;
using Common.Models.Authentication;
using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem;

public class Module : IBaseEntity, IArchiveable
{
    [Key]
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }
    
    public ApplicationUser Owner { get; set; }

    public DateTime? ArchivedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public List<Chapter> Chapters { get; set; } // 1:n Beziehung zu Chapters
    
    public string ModuleName { get; set; }
    public string ModuleDescription { get; set; }
}
