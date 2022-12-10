using System.ComponentModel.DataAnnotations.Schema;
using Common.Models.Authentication;
using Common.Models.HelperInterfaces;

namespace Common.Models;

public class WeatherForecast : IBaseEntity,
    ICreationTimeTracked,
    IArchiveable,
    IModificationTimeTracked
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    [NotMapped] public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
    public DateTime? ArchivedDate { get; set; }
    public DateTime? DeletedDate { get; set; }

    // Interface Implementations
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public ApplicationUser Owner { get; set; } = null!;
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
}