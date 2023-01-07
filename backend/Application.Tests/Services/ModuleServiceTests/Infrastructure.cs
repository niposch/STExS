using Application.Services;
using Common.Models.Authentication;
using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Generic;
using Common.RepositoryInterfaces.Tables;
using Microsoft.AspNetCore.Identity;
using Moq;
using Repositories;
using Repositories.Repositories;
using TestHelper;

namespace Application.Tests.Services.ModuleServiceTests;

public abstract class Infrastructure
{
    protected readonly IFixture Fixture;
    protected readonly ApplicationDbContext ApplicationDbContext;
    protected readonly ModuleService ModuleService;
    protected readonly Mock<UserManager<ApplicationUser>> UserManagerMock;

    protected Infrastructure()
    {
        this.Fixture = new Fixture().Customize(new AutoMoqCustomization());
        this.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        this.UserManagerMock = this.Fixture.FreezeInject<UserManager<ApplicationUser>>();
        
        this.Fixture.Register<IModuleRepository>(this.Fixture.Create<ModuleRepository>);
        this.Fixture.Register<IModuleParticipationRepository>(this.Fixture.Create<ModuleParticipationRepository>);
        
        this.Fixture.Register<IApplicationRepository>(this.Fixture.Create<ApplicationRepository>);
        
        this.ApplicationDbContext = this.Fixture.InjectInMemoryDbContext<ApplicationDbContext>();
        
        this.ModuleService = this.Fixture.Create<ModuleService>();
    }
}