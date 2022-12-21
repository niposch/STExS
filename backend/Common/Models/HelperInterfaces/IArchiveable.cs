using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.HelperInterfaces;

public abstract class ArchiveableBaseEntity : DeletableBaseEntity
{
    [NotMapped]
    public bool IsArchived
    {
        get => this.ArchivedDate.HasValue;
        set
        {
            this.ArchivedDate = value ? DateTime.Now : null;
        }
    }

    public DateTime? ArchivedDate { get; set; }
}