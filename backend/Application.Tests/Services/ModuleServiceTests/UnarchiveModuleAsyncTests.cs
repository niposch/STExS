using Common.Exceptions;
using Common.Models.ExerciseSystem;

namespace Application.Tests.Services.ModuleServiceTests;

public sealed class UnarchiveModuleAsyncTests: Infrastructure
{
    private Guid moduleId;
    [Fact]
    public async Task ShouldUnarchiveArchivedModule()
    {
        // Arrange
        this.moduleId = Guid.NewGuid();
        this.ApplicationDbContext.Modules.Add(
            this.Fixture.Build<Module>()
                .With(m => m.IsArchived, true)
                .Without(m => m.ArchivedDate)
                .With(m => m.Id, this.moduleId)
                .Create());

        this.ApplicationDbContext.SaveChanges();
        
        // Act
        await this.CallAsync();
        
        // Assert
        var module = this.ApplicationDbContext.Modules.Find(this.moduleId);
        module.Should().NotBeNull();
        module!.IsArchived.Should().BeFalse();
        module.ArchivedDate.Should().BeNull();
    }
    
    [Fact]
    public async Task ShouldThrowEntityNotFoundException()
    {
        // Arrange
        this.moduleId = Guid.NewGuid();
        
        // Act
        Func<Task> act = () => this.CallAsync();
        
        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException<Module>>();
    }
    
    [Fact]
    public async Task ShouldKeepUnarchivedModuleUnarchived()
    {
        // Arrange
        this.moduleId = Guid.NewGuid();
        this.ApplicationDbContext.Modules.Add(
            this.Fixture.Build<Module>()
                .With(m => m.IsArchived, false)
                .Without(m => m.ArchivedDate)
                .With(m => m.Id, this.moduleId)
                .Create());
        this.ApplicationDbContext.SaveChanges();
        
        // Act
        await this.CallAsync();
        
        // Assert
        var module = this.ApplicationDbContext.Modules.Find(this.moduleId);
        module!.IsArchived.Should().BeFalse();
        module.ArchivedDate.Should().BeNull();
    }

    private Task CallAsync()
    {
        return this.ModuleService.UnarchiveModuleAsync(this.moduleId);
    }
    
}