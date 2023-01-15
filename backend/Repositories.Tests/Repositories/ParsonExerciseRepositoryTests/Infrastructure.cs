using Repositories.Repositories;
using TestHelper;

namespace Repositories.Tests.Repositories.ParsonExerciseRepositoryTests;

public abstract class Infrastructure
{
    protected readonly ParsonExerciseRepository Repository;

    protected readonly IFixture Fixture;

    protected readonly ApplicationDbContext Context;

    protected Infrastructure()
    {
        this.Fixture = new Fixture().Customize(new AutoMoqCustomization());
        this.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        this.Fixture.Customize(new AppTestingCustomizations());
        this.Context = this.Fixture.InjectInMemoryDbContext<ApplicationDbContext>();
        this.Repository = this.Fixture.Create<ParsonExerciseRepository>();
    }
}