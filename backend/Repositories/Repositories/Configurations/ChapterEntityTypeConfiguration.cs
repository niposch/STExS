using Common.Models.Authentication;
using Common.Models.ExerciseSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Repositories.Configurations;

public class ChapterEntityTypeConfiguration: IEntityTypeConfiguration<Chapter>
{
    public void Configure(EntityTypeBuilder<Chapter> builder)
    {
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.HasOne<ApplicationUser>(x => x.Owner)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(p => p.ParsonExercises)
            .WithOne(e => e.Chapter)
            .OnDelete(DeleteBehavior.Cascade);
    }
}