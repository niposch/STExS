using Application.Helper.Roles;
using AutoFixture.Kernel;
using Common.Exceptions;
using Common.Models.Authentication;
using Common.Models.ExerciseSystem;
using Common.Models.Grading;
using Moq;
using TestHelper.Customizations;

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
        
        // UserSubmission should use NoSpecimen to avoid creating a UserSubmission
        this.Fixture.Customizations.Add(new PropertyTypeExclusion<BaseExercise>());
        this.ApplicationDbContext.Modules.Add(this.Fixture.Build<Module>()
            .With(m => m.Id, this.id)
            .Create());
        this.ApplicationDbContext.SaveChanges();
        this.userId = Guid.NewGuid();
        var user = this.Fixture.Build<ApplicationUser>()
            .With(m => m.Id, this.userId)
            .Create();

        this.UserManagerMock.Setup(m => m.FindByIdAsync(this.userId.ToString()))
            .ReturnsAsync(user);
        
        this.UserManagerMock.Setup(m => m.GetRolesAsync(user))
            .ReturnsAsync(new List<string>{RoleHelper.Admin});
        
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