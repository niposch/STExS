using Common.Models.Authentication;
using Common.Models.ExerciseSystem.Parson;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Repositories.Configurations.Exercises.Parson;

public class ParsonExerciseEntityTypeConfiguration: IEntityTypeConfiguration<ParsonExercise>
{
    public void Configure(EntityTypeBuilder<ParsonExercise> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.HasOne<ApplicationUser>(x => x.Owner)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(p => p.ExpectedSolution)
            .WithOne(p => p.RelatedExercise)
            .OnDelete(DeleteBehavior.Cascade);
    }
}