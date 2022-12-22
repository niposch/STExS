using Common.Models.ExerciseSystem;

namespace Application.Tests.Services.ModuleServiceTests;

public class GetArchivedModulesAsyncTests : Infrastructure
{
    [Fact]
    public async Task RetrievesOnlyArchived()
    {
        // Arrange
        this.ApplicationDbContext.Modules.AddRange(
            new[]
            {
                this.Fixture.Build<Module>()
                    .With(m => m.IsArchived, false)
                    .Without(m => m.ArchivedDate)
                    .Create(),
                this.Fixture.Build<Module>()
                    .With(m => m.IsArchived, false)
                    .Without(m => m.ArchivedDate)
                    .Create(),
                this.Fixture.Build<Module>()
                    .With(m => m.IsArchived, true)
                    .With(m => m.ArchivedDate)
                    .Create(),
                this.Fixture.Build<Module>()
                    .With(m => m.IsArchived, true)
                    .With(m => m.ArchivedDate)
                    .Create(),
                this.Fixture.Build<Module>()
                    .With(m => m.IsArchived, true)
                    .With(m => m.ArchivedDate)
                    .Create()
            });
        this.ApplicationDbContext.SaveChanges();

        // Act
        var res = await this.CallAsync();

        // Assert
        res.Should().HaveCount(3);
        res.Should().AllSatisfy(m => 
            m.IsArchived.Should().BeTrue());
    }

    [Fact]
    public async Task WithNoArchived_ReturnsEmptyCollection()
    {
        // Arrange
        this.ApplicationDbContext.Add(this.Fixture.Build<Module>()
            .Without(m => m.ArchivedDate)
            .With(m => m.IsArchived, false)
            .Create());
        this.ApplicationDbContext.SaveChanges();
        
        // Act
        var res = await this.CallAsync();
        
        // Assert
        res.Should().BeEmpty();
    }

    private Task<IEnumerable<Module>> CallAsync()
    {
        return this.ModuleService.GetArchivedModulesAsync();
    }
}