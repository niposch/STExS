using Common.Models.ExerciseSystem;

namespace Application.Tests.Services.ModuleServiceTests;

public sealed class GetModulesUserIsAcceptedIntoAsyncTests : Infrastructure
{
    private Guid userId;
    [Fact]
    public async Task GetsModulesUserIsAcceptedIntoAsync()
    {
        // Arrange
        var activeModules = new[]
        {
            // Active Modules
            this.Fixture.Build<Module>()
                .With(m => m.IsArchived, false)
                .With(m => m.IsDeleted, false)
                .Without(m => m.ArchivedDate)
                .Without(m => m.DeletedDate)
                .Create(),
            this.Fixture.Build<Module>()
                .With(m => m.IsArchived, false)
                .With(m => m.IsDeleted, false)
                .Without(m => m.ArchivedDate)
                .Without(m => m.DeletedDate)
                .Create(),
            this.Fixture.Build<Module>()
                .With(m => m.IsArchived, false)
                .With(m => m.IsDeleted, false)
                .Without(m => m.ArchivedDate)
                .Without(m => m.DeletedDate)
                .Create()
        };
        var archivedModules = new[]
        {
            this.Fixture.Build<Module>()
                .With(m => m.IsArchived, true)
                .With(m => m.IsDeleted, false)
                .Without(m => m.ArchivedDate)
                .Without(m => m.DeletedDate)
                .Create(),
            this.Fixture.Build<Module>()
                .With(m => m.IsArchived, true)
                .With(m => m.IsDeleted, false)
                .Without(m => m.ArchivedDate)
                .Without(m => m.DeletedDate)
                .Create(),
        };
        var deletedModules = new[]
        {
            this.Fixture.Build<Module>()
                .With(m => m.IsArchived, true)
                .With(m => m.IsDeleted, true)
                .Without(m => m.ArchivedDate)
                .Without(m => m.DeletedDate)
                .Create(),
            this.Fixture.Build<Module>()
                .With(m => m.IsArchived, true)
                .With(m => m.IsDeleted, true)
                .Without(m => m.ArchivedDate)
                .Without(m => m.DeletedDate)
                .Create(),
            
            this.Fixture.Build<Module>()
                .With(m => m.IsArchived, false)
                .With(m => m.IsDeleted, true)
                .Without(m => m.ArchivedDate)
                .Without(m => m.DeletedDate)
                .Create(),
            this.Fixture.Build<Module>()
                .With(m => m.IsArchived, false)
                .With(m => m.IsDeleted, true)
                .Without(m => m.ArchivedDate)
                .Without(m => m.DeletedDate)
                .Create()
        };

        this.userId = Guid.NewGuid();

        var confirmedModules = new[] { activeModules[0], activeModules[1], archivedModules[0] };

        var moduleParticipations = activeModules
            .Concat(archivedModules)
            .Concat(deletedModules)
            .Select(m =>
                this.Fixture.Build<ModuleParticipation>()
                    .With(p => p.ModuleId, m.Id)
                    .With(p => p.UserId, this.userId)
                    .With(p => p.ParticipationConfirmed, confirmedModules.Contains(m))
                    .Without(p => p.Module)
                    .Without(p => p.User)
                    .Create());

        this.ApplicationDbContext.Modules.AddRange(activeModules);
        this.ApplicationDbContext.Modules.AddRange(archivedModules);
        this.ApplicationDbContext.Modules.AddRange(deletedModules);
        this.ApplicationDbContext.ModuleParticipations.AddRange(moduleParticipations);

        this.ApplicationDbContext.SaveChanges();

        // Act
        var res = (await this.CallAsync()).ToList();
        res.Should().HaveCount(3);
        res.Should().Contain(confirmedModules);
    }

    private Task<IEnumerable<Module>> CallAsync()
    {
        return this.ModuleService.GetModulesUserIsAcceptedIntoAsync(this.userId);
    }
}