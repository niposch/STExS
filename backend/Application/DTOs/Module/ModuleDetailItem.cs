namespace Application.DTOs.Module;

public sealed class ModuleDetailItem
{
    public Guid ModuleId { get; set; }

    public Guid OwnerId { get; set; }
    public string OwnerFirstName { get; set; } = string.Empty;
    public string OwnerLastName { get; set; } = string.Empty;

    public string ModuleName { get; set; } = string.Empty;
    public string ModuleDescription { get; set; } = string.Empty;
    public DateTime? ArchivedDate { get; set; }
    public bool IsArchived => this.ArchivedDate.HasValue;

    public List<Guid> ChapterIds { get; set; } = new();

    public bool IsFavorited { get; set; }

    public string teacherName { get; set; }
    
    public DateTime CreationTime { get; set; }
    
    public int? MaxParticipants { get; set; }
}