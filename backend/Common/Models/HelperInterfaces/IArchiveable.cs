using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.HelperInterfaces;

public interface IArchiveable : IDeletable
{
    [NotMapped]
    public bool IsArchived
    {
        get => ArchivedDate.HasValue;
        set => ArchivedDate = value ? DateTime.Now : null;
    }

    public DateTime? ArchivedDate { get; set; }
}