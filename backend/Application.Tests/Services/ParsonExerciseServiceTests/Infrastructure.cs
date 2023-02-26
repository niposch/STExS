using Application.Services.Exercise;
using Common.RepositoryInterfaces.Generic;
using Common.RepositoryInterfaces.Tables;
using Repositories;
using Repositories.Repositories;
using TestHelper;

namespace Application.Tests.Services.ParsonExerciseServiceTests;

public abstract class Infrastructure
{
    protected readonly ApplicationDbContext Context;
    protected readonly IFixture Fixture;

    protected readonly ParsonExerciseService Service;

    public Infrastructure()
    {
        this.Fixture = new Fixture();
        this.Fixture.Customize(new AutoMoqCustomization());
        this.Context = this.Fixture.InjectInMemoryDbContext<ApplicationDbContext>();
        this.Fixture.Inject<IParsonExerciseRepository>(this.Fixture.Create<ParsonExerciseRepository>());
        this.Fixture.Inject<ICommonExerciseRepository>(this.Fixture.Create<CommonExerciseRepository>());
        this.Fixture.Inject<IApplicationRepository>(this.Fixture.Create<ApplicationRepository>());
        this.Service = this.Fixture.Create<ParsonExerciseService>();
    }
}