using Common.RepositoryInterfaces.Tables;

namespace Common.Repositories.Generic;

public interface IApplicationRepository
{
    public IWeatherForecastRepository WeatherForecasts { get; }
}