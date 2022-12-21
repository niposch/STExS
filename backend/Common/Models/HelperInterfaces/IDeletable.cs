using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.HelperInterfaces;

public abstract class DeletableBaseEntity: BaseEntity
{
    [NotMapped]
    public bool IsDeleted
    {
        get => DeletedDate != null;
        set => DeletedDate = value ? DateTime.Now : null;
    }

    public DateTime? DeletedDate { get; set; }
}