using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Tables;

namespace Repositories;

public class ApplicationRepository : IApplicationRepository
{
    public ApplicationRepository(IWeatherForecastRepository weatherForecasts)
    {
        WeatherForecasts = weatherForecasts ?? throw new ArgumentNullException(nameof(weatherForecasts));
    }

    public IWeatherForecastRepository WeatherForecasts { get; set; }
}