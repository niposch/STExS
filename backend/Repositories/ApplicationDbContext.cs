using Common.Models;
using Common.Models.Authentication;
using Common.Models.ExerciseSystem.Parson;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories;

namespace Repositories;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    
    public DbSet<ParsonExercise> ParsonExercises { get; set; }
}