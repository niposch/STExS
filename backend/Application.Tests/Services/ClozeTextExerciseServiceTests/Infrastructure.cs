using Application.Helper;
using Application.Services.Exercise;
using Application.Services.Grading;
using Application.Services.Interfaces;
using AutoFixture.Kernel;
using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Generic;
using Common.RepositoryInterfaces.Tables;
using Repositories;
using Repositories.Repositories;
using Repositories.Repositories.Grading;
using TestHelper;

namespace Application.Tests.Services.ClozeTextExerciseServiceTests;

public abstract class Infrastructure
{
    protected readonly ClozeTextExerciseService ClozeTextExerciseService;
    protected readonly ApplicationDbContext Context;
    protected readonly IFixture Fixture;

    protected Infrastructure()
    {
        this.Fixture = new Fixture();
        this.Fixture.Customize(new AutoMoqCustomization());
        this.Fixture.Customizations.Add(new TypeRelay(typeof(BaseExercise), typeof(ClozeTextExerciseService)));
        this.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        this.Context = this.Fixture.InjectInMemoryDbContext<ApplicationDbContext>();

        this.Fixture.Register<IUserSubmissionService>(this.Fixture.Create<UserSubmissionService>);
        this.Fixture.Register<IClozeTextExerciseRepository>(this.Fixture.Create<ClozeTextExerciseRepository>);
        this.Fixture.Register<IUSerSubmissionRepository>(this.Fixture.Create<UserSubmissionRepository>);
        this.Fixture.Register<IApplicationRepository>(this.Fixture.Create<ApplicationRepository>);
        this.Fixture.Register<IClozeTextHelper>(this.Fixture.Create<ClozeTextHelper>);

        this.ClozeTextExerciseService = this.Fixture.Create<ClozeTextExerciseService>();
    }
}