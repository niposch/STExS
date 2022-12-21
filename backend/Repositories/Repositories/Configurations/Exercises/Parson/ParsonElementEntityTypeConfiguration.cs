using Common.Models.Authentication;
using Common.Models.ExerciseSystem.Parson;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Repositories.Configurations.Exercises.Parson;

public class ParsonElementEntityTypeConfiguration: IEntityTypeConfiguration<ParsonElement>
{
    public void Configure(EntityTypeBuilder<ParsonElement> builder)
    {
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.HasOne<ApplicationUser>(x => x.Owner)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(p => p.RelatedSolution)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}