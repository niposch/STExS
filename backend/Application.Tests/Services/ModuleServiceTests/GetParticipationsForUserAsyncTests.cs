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
            this.CreateModule(false, false),
            this.CreateModule(false, false),
            this.CreateModule(false, false),
            
            this.CreateModule(true, false),
            this.CreateModule(true, false),
            this.CreateModule(true, false),
            
            this.CreateModule(false, true),
            this.CreateModule(false, true),
            this.CreateModule(false, true),
            
            this.CreateModule(true, true),
            this.CreateModule(true, true),
            this.CreateModule(true, true),
        };
        var participations = new[]
        {
            this.CreateModuleParticipation(this.userId, modules[0], true),
            this.CreateModuleParticipation(this.userId, modules[1], false),
            
            this.CreateModuleParticipation(this.userId, modules[3], true),
            this.CreateModuleParticipation(this.userId, modules[4], false),
            
            this.CreateModuleParticipation(this.userId, modules[6], true),
            this.CreateModuleParticipation(this.userId, modules[7], false),
            
            this.CreateModuleParticipation(this.userId, modules[9], true),
            this.CreateModuleParticipation(this.userId, modules[10], false),
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
    private Module CreateModule(bool isArchived, bool isDeleted)
    {
        return this.Fixture.Build<Module>()
            .Without(m => m.ModuleParticipations)
            .Without(m => m.ArchivedDate)
            .Without(m => m.DeletedDate)
            .With(m => m.IsArchived, isArchived)
            .With(m => m.IsDeleted, isDeleted)
            .Create();
    }

    private Task<IEnumerable<Module>> CallAsync()
    {
        return this.ModuleService.GetParticipationsForUserAsync(this.userId);
    }
}