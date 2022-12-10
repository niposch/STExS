using Common.Models.ExerciseSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Repositories.Configurations;

public class ChapterEntityTypeConfiguration: IEntityTypeConfiguration<Chapter>
{
    public void Configure(EntityTypeBuilder<Chapter> builder)
    {
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
    }
}