using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Services.ModuleServiceTests;

public sealed class CreateModuleAsyncTests: Infrastructure
{
    private string moduleName = null!;
    private string moduleDescription = null!;
    private int maxParticipants = 0;
    private Guid ownerId;
    [Fact]
    public async Task CreatesModule()
    {
        // Arrange
        this.moduleName = "Test Module";
        this.moduleDescription = "Test Module Description";
        this.ownerId = Guid.NewGuid();

        // Act
        await this.CallAsync();

        // Assert
        var module = await this.ApplicationDbContext.Modules.FirstOrDefaultAsync();
        module.Should().NotBeNull();
        module!.ModuleName.Should().Be(this.moduleName);
        module.ModuleDescription.Should().Be(this.moduleDescription);
        module.OwnerId.Should().Be(this.ownerId);
    }

    private Task CallAsync()
    {
        return this.ModuleService.CreateModuleAsync(this.moduleName,
            this.moduleDescription, this.maxParticipants,
            this.ownerId);
    }
}