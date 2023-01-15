using Common.Models.Authentication;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.CodeOutput;
using Common.Models.ExerciseSystem.Parson;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Repositories.Configurations.Exercises;

public class BaseExerciseConfiguration: IEntityTypeConfiguration<BaseExercise>
{
    public void Configure(EntityTypeBuilder<BaseExercise> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.HasOne<ApplicationUser>(x => x.Owner)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasDiscriminator<ExerciseType>(nameof(BaseExercise.ExerciseType))
            .HasValue<CodeOutputExercise>(ExerciseType.CodeOutput)
            .HasValue<ParsonExercise>(ExerciseType.Parson);
    }
}