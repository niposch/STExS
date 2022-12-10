using Common.Models.Authentication;
using Common.Models.ExerciseSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Repositories.Configurations;

public class ModuleEntityTypeConfiguration: IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.HasOne<ApplicationUser>(x => x.Owner)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(p => p.Chapters)
            .WithOne(c => c.Module)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}