using Common.Models;
using Common.Models.Authentication;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.Parson;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(builder);
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    
    public DbSet<ParsonExercise> ParsonExercises { get; set; }
    
    public DbSet<ParsonSolution> ParsonSolutions { get; set; }
    
    public DbSet<ParsonElement> ParsonElements { get; set; }
    
    public DbSet<Module> Modules { get; set; }
    
    public DbSet<Chapter> Chapters { get; set; }
    
    public DbSet<ModuleParticipation> ModuleParticipations { get; set; }
}