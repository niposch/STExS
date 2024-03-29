﻿using Application.DTOs.ModuleDTOs;
using Common.Exceptions;
using Common.Models.ExerciseSystem;

namespace Application.Tests.Services.ModuleServiceTests;

public sealed class GetModuleByIdAsync: Infrastructure
{
    private Guid id;
    private Guid userId;

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task GetsModule(bool isArchived)
    {
        // Arrange
        this.id = Guid.NewGuid();
        var expectedModule = this.Fixture.Build<Module>()
            .With(x => x.Id, this.id)
            .Without(m => m.ArchivedDate)
            .With(m => m.IsArchived, isArchived)
            .Create();
        this.ApplicationDbContext.Modules.Add(expectedModule);
        this.ApplicationDbContext.Modules.Add(this.Fixture.Create<Module>());
        this.ApplicationDbContext.SaveChanges();
        
        // Act
        var res = await this.CallAsync();
        
        // Assert
        res.ModuleId.Should().Be(expectedModule.Id);
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

    private Task<ModuleDetailItem> CallAsync()
    {
        return this.ModuleService.GetModuleByIdAsync(this.id, this.userId);
    }
}