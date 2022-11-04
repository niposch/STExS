using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Tables;
using Common.Models;

namespace Repositories.Repositories;

public class WeatherForecastRepository : GenericCrudRepository<WeatherForecast>, IWeatherForecastRepository
{
    public WeatherForecastRepository(ApplicationDbContext context) : base(context)
    {
    }
}