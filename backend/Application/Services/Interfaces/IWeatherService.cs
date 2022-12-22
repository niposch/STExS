using Common.Models;

namespace Application.Services.Interfaces;

public interface IWeatherService
{
    public Task CreateWeatherReport(WeatherForecast newWeatherForecast, CancellationToken cancellationToken = default);
    
    public Task<WeatherForecast> GetWeatherReport(Guid id, CancellationToken cancellationToken = default);
    public Task<List<WeatherForecast>> GetAllActive(CancellationToken cancellationToken = default);
    public Task DeleteWeatherReport(Guid id, CancellationToken cancellationToken = default);
    public Task UpdateWeatherReport(Guid id, WeatherForecast updatedWeatherForecast, CancellationToken cancellationToken = default);
}