using Common.Models.ExerciseSystem;

namespace Application.Tests.Services.ModuleServiceTests;

public class ArchiveModuleAsyncTests: Infrastructure
{
    private Guid moduleId;
    [Fact]
    public async Task UnarchivedModuleShouldBeArchived()
    {
        // Arrange
        var module = this.Fixture.Create<Module>();
        this.moduleId = module.Id;
        
        module.IsArchived = false;
        
        this.ApplicationDbContext.Modules.Add(module);
        this.ApplicationDbContext.SaveChanges();
        
        // Act
        await this.CallAsync();

        // Assert
        var moduleFromDb = this.ApplicationDbContext.Modules.FirstOrDefault(x => x.Id == this.moduleId);
        moduleFromDb.Should().NotBeNull();
        moduleFromDb.IsArchived.Should().BeTrue();
        moduleFromDb.ArchivedDate.Should().BeCloseTo(DateTime.Now, new TimeSpan(0, 1, 0, 0));
    }
    
    [Fact]
    public async Task ArchivedModuleShouldStayArchived()
    {
        // Arrange
        var module = this.Fixture.Create<Module>();
        this.moduleId = module.Id;
        
        module.ArchivedDate = new DateTime(2020,1,1,1,1,1);
        
        this.ApplicationDbContext.Modules.Add(module);
        this.ApplicationDbContext.SaveChanges();
        
        // Act
        await this.CallAsync();

        // Assert
        var moduleFromDb = this.ApplicationDbContext.Modules.FirstOrDefault(x => x.Id == this.moduleId);
        moduleFromDb.Should().NotBeNull();
        moduleFromDb.IsArchived.Should().BeTrue();
        moduleFromDb.ArchivedDate.Should().Be(new DateTime(2020, 1, 1, 1, 1, 1));
    }
    
    private Task CallAsync()
    {
        return this.ModuleService.ArchiveModuleAsync(this.moduleId);
    }
}