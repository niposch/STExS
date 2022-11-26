using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem;

public class Chapter : IBaseEntity, IArchiveable, IDeletable
{
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }

    public DateTime? ArchivedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int RunningNumber { get; set; }

    public List<BaseExercise> Exercises { get; set; } // 1:n Beziehung zu BaseExercise

    // TODO Mahmoud Chapter weitere Felder hinzufügen
}
