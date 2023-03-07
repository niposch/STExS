using Application.Services.Grading;
using Application.Services.Interfaces;
using Application.Services.Permissions;
using AutoFixture.Kernel;
using Common.Models.Authentication;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.CodeOutput;
using Common.RepositoryInterfaces.Generic;
using Common.RepositoryInterfaces.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Repositories;
using Repositories.Repositories.Grading;
using TestHelper;

namespace Application.Tests.Services.GradingServiceTests;

public abstract class Infrastructure
{
    protected readonly IFixture Fixture;
    protected readonly ApplicationDbContext Context;
    protected readonly GradingService Service;
    protected readonly Mock<IAccessService> AccessServiceMock;

    protected Infrastructure()
    {
        this.Fixture = new Fixture();
        this.Fixture.Customize(new AutoMoqCustomization());
        this.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        this.Context = this.Fixture.InjectInMemoryDbContext<ApplicationDbContext>();
        this.Fixture.Register<IAccessService>(this.Fixture.Create<AccessService>);
        this.Fixture.Register<ISubmissionRepository>(this.Fixture.Create < SubmissionRepository>);
        this.Fixture.Register<IApplicationRepository>(this.Fixture.Create<ApplicationRepository>);
        this.AccessServiceMock = this.Fixture.FreezeInject<IAccessService>();
        
        this.Fixture.Customizations.Add(new TypeRelay(typeof(BaseExercise), typeof(CodeOutputExercise)));

        this.Service = this.Fixture.Create<GradingService>();
    }
}