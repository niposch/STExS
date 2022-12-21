using Common.Models.ExerciseSystem;

namespace Application.Tests.Services.ModuleServiceTests;

public sealed class GetModulesAsyncTests : Infrastructure
{
    [Fact]
    public async Task RetrievesOnlyUnarchivedAndUndeletedModules()
    {
        // Arrange
        this.ApplicationDbContext.Modules.AddRange(
            new[]
            {
                // Expected Modules
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

                // Archived Modules
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

                // Deleted Modules
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
                    .Create(),

                // Archived and deleted modules
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
                    .Create()
            });
        this.ApplicationDbContext.SaveChanges();

        // Act
        var res = (await this.CallAsync()).ToList();

        // Assert
        res.Should().NotBeNull();
        res.Should().HaveCount(2);
    }

    [Fact]
    public async Task ShouldReturnEmptyList()
    {
        // Arrange

        // Act
        var res = (await this.CallAsync()).ToList();

        // Assert

        res.Should().NotBeNull();
        res.Should().BeEmpty();
    }

    private async Task<IEnumerable<Module>> CallAsync()
    {
        return await this.ModuleService.GetModulesAsync();
    }
}