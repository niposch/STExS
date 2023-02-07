using Common.Models;
using Common.Models.Authentication;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.CodeOutput;
using Common.Models.ExerciseSystem.Parson;
using Common.Models.Grading;
using Common.Models.HelperInterfaces;
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
        this.ConfigureGrading(builder);
        base.OnModelCreating(builder);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        var creationTimeTracked = ChangeTracker.Entries()
            .Where(e => e.Entity is ICreationTimeTracked && e.State == EntityState.Added);
        creationTimeTracked.ToList()
            .ForEach(e => ((ICreationTimeTracked)e.Entity).CreationTime = DateTime.Now);
        
        var lastModificationTimeTracked = ChangeTracker.Entries()
            .Where(e => e.Entity is IModificationTimeTracked && (e.State == EntityState.Modified || e.State == EntityState.Added));
        lastModificationTimeTracked.ToList()
            .ForEach(e => ((IModificationTimeTracked)e.Entity).ModificationTime = DateTime.Now);
        
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        var creationTimeTracked = ChangeTracker.Entries()
            .Where(e => e.Entity is ICreationTimeTracked && e.State == EntityState.Added);
        creationTimeTracked.ToList()
            .ForEach(e => ((ICreationTimeTracked)e.Entity).CreationTime = DateTime.Now);
        
        var lastModificationTimeTracked = ChangeTracker.Entries()
            .Where(e => e.Entity is IModificationTimeTracked && (e.State == EntityState.Modified || e.State == EntityState.Added));
        lastModificationTimeTracked.ToList()
            .ForEach(e => ((IModificationTimeTracked)e.Entity).ModificationTime = DateTime.Now);
        
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    
    public DbSet<BaseExercise> Exercises { get;set; }
    
    public DbSet<CodeOutputExercise> CodeOutputExercises { get;set; }

    public DbSet<ParsonExercise> ParsonExercises { get; set; }
    
    public DbSet<ParsonSolution> ParsonSolutions { get; set; }
    
    public DbSet<ParsonElement> ParsonElements { get; set; }
    
    public DbSet<Module> Modules { get; set; }
    
    public DbSet<Chapter> Chapters { get; set; }
    
    public DbSet<ModuleParticipation> ModuleParticipations { get; set; }

    #region Grading

    public DbSet<GradingResult> GradingResults { get; set; }
    
    public DbSet<BaseSubmission> Submissions { get; set; }
    
    public DbSet<CodeOutputSubmission> CodeOutputSubmissions { get; set; }

    public DbSet<UserSubmission> UserSubmissions { get; set; }
    
    public DbSet<TimeTrack> TimeTracks { get; set; }
    
    public void ConfigureGrading(ModelBuilder builder)
    {
        builder.Entity<GradingResult>()
            .HasOne(r => r.GradedSubmission)
            .WithOne(s => s.GradingResult)
            .HasForeignKey<BaseSubmission>(s => s.GradingResultId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<UserSubmission>()
            .HasMany(s => s.TimeTracks)
            .WithOne(t => t.UserSubmission)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<UserSubmission>()
            .HasMany(s => s.GradingResults)
            .WithOne(r => r.UserSubmission)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<UserSubmission>()
            .HasMany(s => s.Submissions)
            .WithOne(s => s.UserSubmission)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<UserSubmission>()
            .HasKey(s => new
            {
                s.UserId,
                s.ExerciseId
            });
        
        builder.Entity<BaseExercise>()
            .HasMany(e => e.UserSubmissions)
            .WithOne(s => s.Exercise)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<BaseSubmission>()
            .HasDiscriminator(s => s.SubmissionType)
            .HasValue<CodeOutputSubmission>(ExerciseType.CodeOutput);
        
        builder.Entity<TimeTrack>()
            .HasOne(t => t.UserSubmission)
            .WithMany(s => s.TimeTracks)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(t => new
            {
                t.UserId,
                t.ExerciseId
            });
        
    }
    #endregion
}