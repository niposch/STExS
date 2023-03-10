using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem.Parson;

public sealed class ParsonElement : DeletableBaseEntity, ICreationTimeTracked, IModificationTimeTracked
{
    public string Code { get; set; } = string.Empty;

    public int Indentation { get; set; } = 0;

    // Relationships
    public ParsonSolution RelatedSolution { get; set; } = null!;
    public Guid RelatedSolutionId { get; set; }
    public int RunningNumber { get; set; }

    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }

    public List<ParsonPuzzleAnswerItem> ReferencedInAnswerItems { get; set; } = new();
}