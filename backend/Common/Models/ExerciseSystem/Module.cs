using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem;

public class Module : IBaseEntity, IArchiveable, IDeletable
{
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }

    public DateTime? ArchivedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public List<Chapter> Chapters { get; set; } // 1:n Beziehung zu Chapters

    // TODO Mahmoud Chapter weitere Felder hinzufügen
}
