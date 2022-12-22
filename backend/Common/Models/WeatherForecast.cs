using System.ComponentModel.DataAnnotations.Schema;
using Common.Models.Authentication;
using Common.Models.HelperInterfaces;

namespace Common.Models;

public class WeatherForecast : ArchiveableBaseEntity,
    ICreationTimeTracked,
    IModificationTimeTracked
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    [NotMapped] public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
}