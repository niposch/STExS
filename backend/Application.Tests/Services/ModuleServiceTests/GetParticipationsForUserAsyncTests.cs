using Common.Models.ExerciseSystem;

namespace Application.Tests.Services.ModuleServiceTests;

public sealed class GetParticipationsForUserAsyncTests: Infrastructure
{
    private Guid userId;

    [Fact]
    public async Task ShouldReturnModulesUserIsAcceptedAndInvitedInto()
    {
        // Arrange
        this.userId = Guid.NewGuid();
        var modules = new[]
        {
            this.CreateModule(false),
            this.CreateModule(false),
            this.CreateModule(false),
            
            this.CreateModule(true),
            this.CreateModule(true),
            this.CreateModule(true)
        };
        var participations = new[]
        {
            this.CreateModuleParticipation(this.userId, modules[0], true),
            this.CreateModuleParticipation(this.userId, modules[1], false),
            
            this.CreateModuleParticipation(this.userId, modules[3], true),
            this.CreateModuleParticipation(this.userId, modules[4], false),
        };
        
        this.ApplicationDbContext.Modules.AddRange(modules);
        this.ApplicationDbContext.ModuleParticipations.AddRange(participations);
        this.ApplicationDbContext.SaveChanges();
        
        // Act
        var result = (await this.CallAsync()).ToList();
        
        // Assert
        result.Should().HaveCount(4);
        result.Should().Contain(x => x.Id == modules[0].Id);
        result.Should().Contain(x => x.Id == modules[1].Id);
        result.Should().Contain(x => x.Id == modules[3].Id);
        result.Should().Contain(x => x.Id == modules[4].Id);
    }

    private ModuleParticipation CreateModuleParticipation(Guid userId, Module module, bool isAccepted)
    {
        return new ModuleParticipation
        {
            UserId = userId,
            ModuleId = module.Id,
            ParticipationConfirmed = isAccepted,
        };
    }
    private Module CreateModule(bool isArchived)
    {
        return this.Fixture.Build<Module>()
            .Without(m => m.ModuleParticipations)
            .Without(m => m.ArchivedDate)
            .With(m => m.IsArchived, isArchived)
            .Create();
    }

    private Task<IEnumerable<Module>> CallAsync()
    {
        return this.ModuleService.GetParticipationsForUserAsync(this.userId);
    }
}