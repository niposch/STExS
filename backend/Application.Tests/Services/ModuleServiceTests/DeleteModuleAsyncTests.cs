using Common.Exceptions;
using Common.Models.ExerciseSystem;

namespace Application.Tests.Services.ModuleServiceTests;

public sealed class DeleteModuleAsyncTests: Infrastructure
{
    private Guid id;
    [Fact]
    public async Task DeletesModule()
    {
        // Arrange
        this.id = Guid.NewGuid();
        this.ApplicationDbContext.Modules.Add(this.Fixture.Build<Module>()
            .With(m => m.Id, this.id)
            .Without(m => m.DeletedDate)
            .Without(m => m.IsDeleted)
            .Create());
        this.ApplicationDbContext.SaveChanges();
        
        // Act
        await this.CallAsync();

        // Assert
        var module = this.ApplicationDbContext.Modules.FirstOrDefault(m => m.Id == this.id);
        module.Should().NotBeNull();
        module.IsDeleted.Should().BeTrue();
        module.DeletedDate.Should().BeCloseTo(DateTime.Now, new TimeSpan(0,1,0,0));
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

    [Fact]
    public async Task DeletedModuleStaysDeleted()
    {
        // Arrange
        this.id = Guid.NewGuid();
        this.ApplicationDbContext.Modules.Add(this.Fixture.Build<Module>()
            .With(m => m.Id, this.id)
            .With(m => m.DeletedDate, new DateTime(2020, 1,1))
            .Create());
        this.ApplicationDbContext.SaveChanges();
        
        // Act
        await this.CallAsync();
        
        // Assert
        var module = this.ApplicationDbContext.Modules.FirstOrDefault(m => m.Id == this.id);
        module.Should().NotBeNull();
        module.IsDeleted.Should().BeTrue();
        module.DeletedDate.Should().Be(new DateTime(2020, 1,1));
    }

    private Task CallAsync()
    {
        return this.ModuleService.DeleteModuleAsync(this.id);
    }
}