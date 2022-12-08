using Application.Interfaces.Repositories.Tables;
using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models;

namespace Application.Services;

public class WeatherService: IWeatherService
{
    private readonly IWeatherForecastRepository weatherForecastRepository;

    public WeatherService(IWeatherForecastRepository weatherForecastRepository)
    {
        this.weatherForecastRepository = weatherForecastRepository ??
                                         throw new ArgumentNullException(nameof(weatherForecastRepository));
    }

    public async Task CreateWeatherReport(WeatherForecast newWeatherForecast, CancellationToken cancellationToken = default)
    {
        await weatherForecastRepository.AddAsync(newWeatherForecast, cancellationToken);
    }

    public async Task<List<WeatherForecast>> GetWeatherForecast(CancellationToken cancellationToken = default)
    {
        return await weatherForecastRepository.GetAllActive(cancellationToken);
    }

    public async Task<List<WeatherForecast>> GetAllDeleted(CancellationToken cancellationToken = default)
    {
        return await weatherForecastRepository.GetAllDeleted(cancellationToken);
    }

    public async Task DeleteWeatherReport(Guid id, CancellationToken cancellationToken = default)
    {
        await weatherForecastRepository.Delete(id, cancellationToken);
    }

    public async Task UpdateWeatherReport(Guid id, WeatherForecast updatedWeatherForecast,
        CancellationToken cancellationToken = default)
    {
        await  weatherForecastRepository.UpdateAsync(updatedWeatherForecast, cancellationToken);
    }

    public async Task<WeatherForecast> GetWeatherReport(Guid id, CancellationToken cancellationToken = default)
    {
        return await weatherForecastRepository.TryGetByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException(id, typeof(WeatherForecast));
    }

    public async Task<List<WeatherForecast>> GetAllActive(CancellationToken cancellationToken = default)
    {
        return await weatherForecastRepository.GetAllActive(cancellationToken);
    }
}