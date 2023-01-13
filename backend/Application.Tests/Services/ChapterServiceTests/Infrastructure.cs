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

namespace Application.Tests.Services.ChapterServiceTests;

public abstract class Infrastructure
{
    protected readonly IFixture Fixture;
    protected readonly ApplicationDbContext ApplicationDbContext;
    protected readonly ChapterService ChapterService;
    protected readonly Mock<UserManager<ApplicationUser>> UserManagerMock;
    protected readonly Mock<ModuleRepository> ModuleRepositoryMock;

    protected Infrastructure()
    {
        this.Fixture = new Fixture().Customize(new AutoMoqCustomization());
        this.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        this.UserManagerMock = this.Fixture.FreezeInject<UserManager<ApplicationUser>>();
        this.ModuleRepositoryMock = this.Fixture.FreezeInject<ModuleRepository>();
        
        this.Fixture.Register<IChapterRepository>(this.Fixture.Create<ChapterRepository>);
        
        this.Fixture.Register<IApplicationRepository>(this.Fixture.Create<ApplicationRepository>);
        
        this.ApplicationDbContext = this.Fixture.InjectInMemoryDbContext<ApplicationDbContext>();
        
        this.ChapterService = this.Fixture.Create<ChapterService>();
    }
}