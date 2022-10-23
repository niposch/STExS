using Application.Interfaces.Repositories.Tables;

namespace Application.Interfaces.Repositories;

public interface IApplicationRepository
{
    public IWeatherForecastRepository WeatherForecasts { get; }
}