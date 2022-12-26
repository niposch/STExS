using Common.Exceptions;
using Common.Models.ExerciseSystem;

namespace Application.Tests.Services.ModuleServiceTests;

public sealed class DeleteModuleAsyncTests: Infrastructure
{
    private Guid id;
    private Guid userId;
    
    [Fact]
    public async Task DeletesModule()
    {
        // Arrange
        this.id = Guid.NewGuid();
        this.ApplicationDbContext.Modules.Add(this.Fixture.Build<Module>()
            .With(m => m.Id, this.id)
            .Create());
        this.ApplicationDbContext.SaveChanges();
        
        // Act
        await this.CallAsync();

        // Assert
        var module = this.ApplicationDbContext.Modules.FirstOrDefault(m => m.Id == this.id);
        module.Should().BeNull();
    }

    [Fact]
    public async Task ThrowsNotFoundException()
    {
        // Arrange
        this.id = Guid.NewGuid();
        
        // Act
        Func<Task> act = async () => await this.CallAsync();

        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException<Module>>();
    }

    private Task CallAsync()
    {
        return this.ModuleService.DeleteModuleAsync(this.id, this.userId);
    }
}