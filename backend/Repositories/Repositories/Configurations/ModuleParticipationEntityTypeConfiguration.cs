using Common.Models.ExerciseSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Repositories.Configurations;

public sealed class ModuleParticipationEntityTypeConfiguration:IEntityTypeConfiguration<ModuleParticipation>
{
    public void Configure(EntityTypeBuilder<ModuleParticipation> builder)
    {
        builder.HasOne(b => b.Module)
            .WithMany(b => b.ModuleParticipations)
            .HasForeignKey(b => b.ModuleId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.User)
            .WithMany(b => b.ModuleParticipations)
            .HasForeignKey(b => b.ModuleId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasKey(p => new
        {
            p.ModuleId,
            p.UserId
        });
    }
}