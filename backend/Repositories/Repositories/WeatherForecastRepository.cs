using Application.Interfaces.Repositories.Tables;
using Common.Models;
using Repositories.Repositories.GenericImplementations;

namespace Repositories.Repositories;

public class WeatherForecastRepository : GenericCrudRepository<WeatherForecast>, IWeatherForecastRepository
{
    public WeatherForecastRepository(ApplicationDbContext context) : base(context)
    {
    }
}