using Application.Services.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace STExS.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    private readonly IWeatherService weatherService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,
        IWeatherService weatherService)
    {
        _logger = logger;
        this.weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
    }

    [HttpGet(Name = "GetWeatherForecast")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WeatherForecast>))]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        return Ok(await weatherService.GetAllActive());
    }
}