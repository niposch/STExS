using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
}