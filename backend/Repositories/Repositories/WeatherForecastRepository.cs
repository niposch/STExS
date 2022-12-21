using Common.Models;
using Common.RepositoryInterfaces.Tables;
using Repositories.Repositories.GenericImplementations;

namespace Repositories.Repositories;

public class WeatherForecastRepository : GenericCrudRepository<WeatherForecast>, IWeatherForecastRepository
{
    public WeatherForecastRepository(ApplicationDbContext context) : base(context)
    {
    }
}