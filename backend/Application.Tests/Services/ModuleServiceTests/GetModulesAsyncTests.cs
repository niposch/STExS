using Common.Models.ExerciseSystem;

namespace Application.Tests.Services.ModuleServiceTests;

public sealed class GetModulesAsyncTests : Infrastructure
{
    [Fact]
    public async Task QueriesArchivedAndUnarchivedModules()
    {
        // Arrange
        this.ApplicationDbContext.Modules.AddRange(
            new[]
            {
                // Expected Modules
                this.Fixture.Build<Module>()
                    .With(m => m.IsArchived, false)
                    .Without(m => m.ArchivedDate)
                    .Create(),
                this.Fixture.Build<Module>()
                    .With(m => m.IsArchived, false)
                    .Without(m => m.ArchivedDate)
                    .Create(),

                // Archived Modules
                this.Fixture.Build<Module>()
                    .With(m => m.IsArchived, true)
                    .Without(m => m.ArchivedDate)
                    .Create(),
                this.Fixture.Build<Module>()
                    .With(m => m.IsArchived, true)
                    .Without(m => m.ArchivedDate)
                    .Create(),
            });
        this.ApplicationDbContext.SaveChanges();

        // Act
        var res = (await this.CallAsync()).ToList();

        // Assert
        res.Should().NotBeNull();
        res.Should().HaveCount(4);
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