using Common.Models.ExerciseSystem.Parson;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Repositories.Configurations.Exercises.Parson;

public class ParsonExerciseEntityTypeConfiguration: IEntityTypeConfiguration<ParsonExercise>
{
    public void Configure(EntityTypeBuilder<ParsonExercise> builder)
    {
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.HasOne<ParsonSolution>(e => e.ExpectedSolution)
            .WithOne();
    }
}