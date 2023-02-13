using Application.Services;
using Application.Services.Interfaces;
using Common.RepositoryInterfaces.Generic;
using Common.RepositoryInterfaces.Tables;
using Repositories;
using Repositories.Repositories;
using TestHelper;

namespace Application.Tests.Services.ExerciseServiceTests;

public abstract class Infrastructure
{
    protected readonly IFixture Fixture;
    protected readonly ApplicationDbContext ApplicationDbContext;
    protected readonly ExerciseService ExerciseService;
    
    protected Infrastructure()
    {
        this.Fixture = new Fixture()
            .Customize(new AutoMoqCustomization());
        this.Fixture.Customize(new AppTestingCustomizations());
        this.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        this.ApplicationDbContext = this.Fixture.InjectInMemoryDbContext<ApplicationDbContext>();
        this.Fixture.Inject<ICommonExerciseRepository>(this.Fixture.Create<CommonExerciseRepository>());
        this.Fixture.Inject<IApplicationRepository>(this.Fixture.Create<ApplicationRepository>());
        this.ExerciseService = this.Fixture.Create<ExerciseService>();
    }
}