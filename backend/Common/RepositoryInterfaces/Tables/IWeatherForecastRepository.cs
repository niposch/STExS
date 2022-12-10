using Common.Models;
using Common.Repositories.Generic;

namespace Common.RepositoryInterfaces.Tables;

public interface IWeatherForecastRepository : IGenericCrudRepository<WeatherForecast>
{
}