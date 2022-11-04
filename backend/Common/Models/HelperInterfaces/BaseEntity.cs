using System.ComponentModel.DataAnnotations;

namespace Common.Models.HelperInterfaces;

public interface IBaseEntity
{
    [Key] public Guid Id { get; set; }

    public Guid OwnerId { get; set; }
}