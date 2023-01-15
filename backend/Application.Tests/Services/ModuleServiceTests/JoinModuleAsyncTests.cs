using Common.Models.Authentication;
using Common.Models.ExerciseSystem;

namespace Application.Tests.Services.ModuleServiceTests;

public class JoinModuleAsyncTests : Infrastructure
{
    private Guid moduleId;
    private Guid userId;

    [Fact]
    public async Task WhenSuccessful_ShouldJoinModuleAndReturnSuccess()
    {
        // Arrange
        this.userId = Guid.NewGuid();
        var module = this.CreateModule(false, false, this.userId, 100, false);
        this.moduleId = module.Id;
        this.ApplicationDbContext.SaveChanges();

        // Act
        var result = await this.CallAsync();

        // Assert
        result.Should().Be(JoinResult.JoinedSucessfully);
        this.ApplicationDbContext.ModuleParticipations.Should()
            .HaveCount(1);
        var participation = this.ApplicationDbContext.ModuleParticipations.First();
        var moduleParticipation = this.ApplicationDbContext.ModuleParticipations.First();
        participation.UserId.Should().Be(this.userId);
        participation.ModuleId.Should().Be(this.moduleId);
    }

    [Fact]
    public async Task WhenModuleIsFull_ReturnsModuleIsFull()
    {
        // Arrange
        this.userId = Guid.NewGuid();
        var module = this.CreateModule(false, false, this.userId, 1, false);
        this.moduleId = module.Id;
        this.ApplicationDbContext.ModuleParticipations.Add(this.Fixture.Build<ModuleParticipation>()
            .Without(m => m.Module)
            .Without(m => m.User)
            .With(m => m.ModuleId, this.moduleId)
            .Create());
        this.ApplicationDbContext.SaveChanges();
        
        // Act
        var result = await this.CallAsync();
        
        // Assert
        result.Should().Be(JoinResult.ModuleIsFull);
    }

    [Fact]
    public async Task WhenUserRequestedParticipation_ReturnsVerificationPending()
    {
        // Arrange
        this.userId = Guid.NewGuid();
        var module = this.CreateModule(false, true, this.userId, 100, false);
        this.moduleId = module.Id;
        this.ApplicationDbContext.SaveChanges();
        
        // Act
        var result = await this.CallAsync();
        
        // Assert
        result.Should().Be(JoinResult.VerificationPending);
    }

    [Fact]
    public async Task WhenModuleDoesntExist_ReturnsModuleDoesntExistResult()
    {
        // Arrange
        this.userId = Guid.NewGuid();
        this.moduleId = Guid.NewGuid();
        
        // Act
        var result = await this.CallAsync();
        
        // Assert
        result.Should().Be(JoinResult.ModuleDoesNotExist);
    }

    [Fact]
    public async Task WhenModuleIsArchived_ReturnsModuleIsArchivedResult()
    {
        // Arrange
        this.userId = Guid.NewGuid();
        var module = this.CreateModule(true, false, this.userId, 100, false);
        this.moduleId = module.Id;
        this.ApplicationDbContext.SaveChanges();
        
        // Act
        var result = await this.CallAsync();
        
        // Assert
        result.Should().Be(JoinResult.ModuleIsArchived);
    }

    [Fact]
    public async Task WhenAlreadyAcceptedIntoModule_ReturnsAlreadyAcceptedIntoModuleResult()
    {
        // Arrange
        this.userId = Guid.NewGuid();
        var module = this.CreateModule(false, true, this.userId, 100, true);
        this.moduleId = module.Id;
        this.ApplicationDbContext.SaveChanges();
        
        // Act
        var result = await this.CallAsync();
        
        // Assert
        result.Should().Be(JoinResult.AlreadyJoined);
    }

    private Module CreateModule(bool isArchived,
        bool isJoined,
        Guid? userId,
        int maxMemberCount,
        bool isConfirmed = false)
    {
        var module = this.Fixture.Build<Module>()
            .With(m => m.MaxParticipants, maxMemberCount)
            .Without(m => m.ModuleParticipations)
            .With(m => m.Owner, this.Fixture.Build<ApplicationUser>().Without(o => o.ModuleParticipations).Create())
            .Without(m => m.Chapters)
            .With(m => m.IsArchived, isArchived)
            .Create();
        this.ApplicationDbContext.Modules.Add(module);
        if (isJoined)
        {
            var moduleParticipation = this.Fixture.Build<ModuleParticipation>()
                .With(m => m.ModuleId, module.Id)
                .With(m => m.UserId, userId)
                .Without(m => m.Module)
                .Without(m => m.User)
                .With(m => m.ParticipationConfirmed, isConfirmed)
                .Create();
            this.ApplicationDbContext.ModuleParticipations.Add(moduleParticipation);
        }

        return module;
    }

    private Task<JoinResult> CallAsync()
    {
        return this.ModuleService.JoinModuleAsync(this.moduleId, this.userId);
    }
}