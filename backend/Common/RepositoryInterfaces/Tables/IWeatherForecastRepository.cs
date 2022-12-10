using Common.Models;
using Common.RepositoryInterfaces.Generic;

namespace Common.RepositoryInterfaces.Tables;

public interface IWeatherForecastRepository : IGenericCrudRepository<WeatherForecast>
{
}