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
        var module = this.CreateModule(false, false, this.userId, 100);
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
    }

    [Fact]
    public async Task WhenUserIsAlreadyInModule_ReturnsUserIsAlreadyInModuleResult()
    {
    }

    [Fact]
    public async Task WhenModuleDoesntExist_ReturnsModuleDoesntExistResult()
    {
    }

    [Fact]
    public async Task WhenModuleIsArchived_ReturnsModuleIsArchivedResult()
    {
    }

    [Fact]
    public async Task WhenAlreadyJoinedModule_ReturnsAlreadyJoinedModuleResult()
    {
    }

    [Fact]
    public async Task WhenAlreadyAcceptedIntoModule_ReturnsAlreadyAcceptedIntoModuleResult()
    {
    }

    private Module CreateModule(bool isArchived,
        bool isJoined,
        Guid? userId,
        int maxMemberCount)
    {
        var module = this.Fixture.Build<Module>()
            .With(m => m.MaxParticipants, maxMemberCount)
            .Without(m => m.Owner)
            .Without(m => m.ModuleParticipations)
            .Without(m => m.Owner)
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