using Application.DTOs.ModuleDTOs;
using Common.Models.ExerciseSystem;

namespace Application.Tests.Services.ModuleServiceTests;

public sealed class GetActiveModulesAsyncTests: Infrastructure
{
    Guid ownerId = Guid.NewGuid();
    private Guid userId;

    [Fact]
    public async Task GetsUnarchivedModulesOnly()
    {
        // Arrange
        var modules = new[]
        {
            this.CreateModule(false),
            this.CreateModule(false),
            this.CreateModule(false),
            
            this.CreateModule(true),
            this.CreateModule(true),
            this.CreateModule(true),
        };
        this.ApplicationDbContext.Modules.AddRange(modules);
        this.ApplicationDbContext.SaveChanges();
        
        // Act
        var res = await this.CallAsync();
        
        // Assert
        res.Should().HaveCount(3);
    }

    private Module CreateModule(bool isArchived)
    {
        return this.Fixture.Build<Module>()
            .With(m => m.IsArchived, isArchived)
            .With(m => m.OwnerId, this.ownerId)
            .Without(m => m.ArchivedDate)
            .Create();
    }
    private Task<IEnumerable<ModuleDetailItem>> CallAsync()
    {
        return this.ModuleService.GetActiveModulesAsync(this.userId);
    }
}