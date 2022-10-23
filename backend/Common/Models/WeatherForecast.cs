using System.ComponentModel.DataAnnotations.Schema;
using Common.Models.HelperInterfaces;

namespace Common.Models;

public class WeatherForecast : IBaseEntity,
    ICreationTimeTracked,
    IArchiveable,
    IDeletable,
    IModificationTimeTracked
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
    public DateTime? ArchivedDate { get; set; }
    public DateTime? DeletedDate { get; set; }

    // Interface Implementations
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
}