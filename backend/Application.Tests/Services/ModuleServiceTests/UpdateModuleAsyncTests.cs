using Common.Exceptions;
using Common.Models.ExerciseSystem;

namespace Application.Tests.Services.ModuleServiceTests;

public sealed class UpdateModuleAsyncTests: Infrastructure
{
    private Guid moduleId;
    private string newName;
    private string newDescription;
    private int maxParticipants;

    [Fact]
    public async Task ChangesPersist()
    {
        // Arrange
        this.moduleId = Guid.NewGuid();
        this.newName = this.Fixture.Create<string>();
        this.newDescription = this.Fixture.Create<string>();
        this.ApplicationDbContext.Modules.Add(
            this.Fixture.Build<Module>()
                .With(m => m.Id, this.moduleId)
                .Create());
        this.ApplicationDbContext.SaveChanges();
        
        // Act
        await this.CallAsync();
        
        // Assert
        var module = this.ApplicationDbContext.Modules.Find(this.moduleId);
        module.Should().NotBeNull();
        module.ModuleName.Should().Be(this.newName);
        module.ModuleDescription.Should().Be(this.newDescription);
    }
    
    [Fact]
    public async Task ThrowsEntityNotFoundException()
    {
        // Arrange
        this.moduleId = Guid.NewGuid();
        
        // Act
        Func<Task> act = async () => await this.CallAsync();
        
        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException<Module>>();
    }

    private Task CallAsync()
    {
        return this.ModuleService.UpdateModuleAsync(this.moduleId, this.newName, this.newDescription, this.maxParticipants);
    }
}