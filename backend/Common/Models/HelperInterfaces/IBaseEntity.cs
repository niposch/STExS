using System.ComponentModel.DataAnnotations;
using Common.Models.Authentication;

namespace Common.Models.HelperInterfaces;

public interface IBaseEntity
{
    [Key] public Guid Id { get; set; }

    public Guid OwnerId { get; set; }
    
    public ApplicationUser Owner { get; set; }
}