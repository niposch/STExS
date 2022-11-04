using Common.Models;

namespace Application.Interfaces.Repositories.Tables;

public interface IWeatherForecastRepository : IGenericCrudRepository<WeatherForecast>
{
}