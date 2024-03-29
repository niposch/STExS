﻿using Application.DTOs.ModuleDTOs;
using Common.Models.ExerciseSystem;

namespace Application.Tests.Services.ModuleServiceTests;

public sealed class GetModulesUserIsAcceptedIntoAsyncTests : Infrastructure
{
    private Guid userId;
    [Fact]
    public async Task GetsModulesUserIsAcceptedIntoAsync()
    {
        // Arrange
        var activeModules = new[]
        {
            // Active Modules
            this.Fixture.Build<Module>()
                .With(m => m.IsArchived, false)
                .Without(m => m.ArchivedDate)
                .Create(),
            this.Fixture.Build<Module>()
                .With(m => m.IsArchived, false)
                .Without(m => m.ArchivedDate)
                .Create(),
            this.Fixture.Build<Module>()
                .With(m => m.IsArchived, false)
                .Without(m => m.ArchivedDate)
                .Create()
        };
        var archivedModules = new[]
        {
            this.Fixture.Build<Module>()
                .With(m => m.IsArchived, true)
                .Without(m => m.ArchivedDate)
                .Create(),
            this.Fixture.Build<Module>()
                .With(m => m.IsArchived, true)
                .Without(m => m.ArchivedDate)
                .Create(),
        };

        this.userId = Guid.NewGuid();

        var confirmedModules = new[] { activeModules[0], activeModules[1], archivedModules[0] };

        var moduleParticipations = activeModules
            .Concat(archivedModules)
            .Select(m =>
                this.Fixture.Build<ModuleParticipation>()
                    .With(p => p.ModuleId, m.Id)
                    .With(p => p.UserId, this.userId)
                    .With(p => p.ParticipationConfirmed, confirmedModules.Contains(m))
                    .Without(p => p.Module)
                    .Without(p => p.User)
                    .Create());

        this.ApplicationDbContext.Modules.AddRange(activeModules);
        this.ApplicationDbContext.Modules.AddRange(archivedModules);
        this.ApplicationDbContext.ModuleParticipations.AddRange(moduleParticipations);

        this.ApplicationDbContext.SaveChanges();

        // Act
        var res = (await this.CallAsync()).ToList();
        res.Should().HaveCount(3);
        res.Select(m => m.ModuleId).Should().Contain(confirmedModules.Select(m => m.Id));
    }

    private Task<IEnumerable<ModuleDetailItem>> CallAsync()
    {
        return this.ModuleService.GetModulesUserIsAcceptedIntoAsync(this.userId);
    }
}