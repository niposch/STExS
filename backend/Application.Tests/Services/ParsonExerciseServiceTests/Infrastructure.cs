using Application.Services.Exercise;
using AutoFixture.Kernel;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.Parson;
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

    protected Infrastructure()
    {
        this.Fixture = new Fixture();
        this.Fixture.Customize(new AutoMoqCustomization());
        this.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        this.Fixture.Customizations.Add(new TypeRelay(typeof(BaseExercise), typeof(ParsonExercise)));
        this.Context = this.Fixture.InjectInMemoryDbContext<ApplicationDbContext>();
        this.Fixture.Inject<IParsonElementRepository>(this.Fixture.Create<ParsonElementRepository>());
        this.Fixture.Inject<IParsonExerciseRepository>(this.Fixture.Create<ParsonExerciseRepository>());
        this.Fixture.Inject<ICommonExerciseRepository>(this.Fixture.Create<CommonExerciseRepository>());
        this.Fixture.Inject<IApplicationRepository>(this.Fixture.Create<ApplicationRepository>());
        this.Service = this.Fixture.Create<ParsonExerciseService>();
    }
}