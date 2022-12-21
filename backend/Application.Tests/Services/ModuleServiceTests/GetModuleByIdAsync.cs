using Common.Exceptions;
using Common.Models.ExerciseSystem;

namespace Application.Tests.Services.ModuleServiceTests;

public sealed class GetModuleByIdAsync: Infrastructure
{
    private Guid id;
    [Theory]
    [InlineData(false, false)]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(true, true)]
    public async Task GetsModule(bool isArchived, bool isDeleted)
    {
        // Arrange
        this.id = Guid.NewGuid();
        var expectedModule = this.Fixture.Build<Module>()
            .With(x => x.Id, this.id)
            .Without(m => m.DeletedDate)
            .Without(m => m.ArchivedDate)
            .With(m => m.IsArchived, isArchived)
            .With(m => m.IsDeleted, isDeleted)
            .Create();
        this.ApplicationDbContext.Modules.Add(expectedModule);
        this.ApplicationDbContext.Modules.Add(this.Fixture.Create<Module>());
        this.ApplicationDbContext.SaveChanges();
        
        // Act
        var res = await this.CallAsync();
        
        // Assert
        res.Should().Be(expectedModule);
    }
    
    [Fact]
    public async Task ThrowsEntityNotFoundException()
    {
        // Arrange
        this.id = Guid.NewGuid();
        
        // Act
        Func<Task> act = async () => await this.CallAsync();
        
        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException<Module>>();
    }

    private Task<Module> CallAsync()
    {
        return this.ModuleService.GetModuleByIdAsync(this.id);
    }
}