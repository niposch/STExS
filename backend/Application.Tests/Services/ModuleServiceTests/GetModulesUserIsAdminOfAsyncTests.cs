using Common.Models.ExerciseSystem;

namespace Application.Tests.Services.ModuleServiceTests;

public sealed class GetModulesUserIsAdminOfAsyncTests: Infrastructure
{
    private Guid userId;

    [Fact]
    public async Task RetrievesAllModulesWhereUserIsAdmin()
    {
        // Arrange
        this.userId = Guid.NewGuid();
        // create 3 modules user is owner of
        for (var i = 0; i < 3; i++)
        {
            this.AddModuleToDb(this.userId);
        }
        
        // create 3 modules user is not owner of
        for (var i = 0; i < 3; i++)
        {
            this.AddModuleToDb(Guid.NewGuid());
        }

        this.ApplicationDbContext.SaveChanges();
        
        // Act
        var res = await this.CallAsync();

        // Assert
        res.Should().HaveCount(3);
    }
    
    [Fact]
    public async Task ReturnsEmptyListIfNoModulesFound()
    {
        // Arrange
        this.userId = Guid.NewGuid();
        // create 3 modules user is not owner of
        for (var i = 0; i < 3; i++)
        {
            this.AddModuleToDb(Guid.NewGuid());
        }
        
        // Act
        var res = await this.CallAsync();

        // Assert
        res.Should().BeEmpty();
    }

    private void AddModuleToDb(Guid ownerId)
    {
        var module = this.Fixture.Build<Module>()
            .With(m => m.OwnerId, ownerId)
            .Without(m => m.Owner)
            .Create();
        this.ApplicationDbContext.Modules.Add(module);
    }
    
    private Task<List<Module>> CallAsync()
    {
        return this.ModuleService.GetModulesUserIsAdminOfAsync(this.userId);
    }
}