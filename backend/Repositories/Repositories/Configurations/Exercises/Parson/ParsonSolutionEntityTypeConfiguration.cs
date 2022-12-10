using Common.Models.ExerciseSystem.Parson;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Repositories.Configurations.Exercises.Parson;

public class ParsonSolutionEntityTypeConfiguration: IEntityTypeConfiguration<ParsonSolution>
{
    public void Configure(EntityTypeBuilder<ParsonSolution> builder)
    {
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
    }
}