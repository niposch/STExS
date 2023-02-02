using Repositories.Repositories.Grading;
using TestHelper;
namespace Repositories.Tests.Repositories.GradingResultRepositoryTests;

public class Infrastructure
{
    protected readonly GradingResultRepository Repository;

    protected readonly IFixture Fixture;

    protected readonly ApplicationDbContext Context;

    protected Infrastructure()
    {
        this.Fixture = new Fixture().Customize(new AutoMoqCustomization());
        this.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        this.Fixture.Customize(new AppTestingCustomizations());
        this.Context = this.Fixture.InjectInMemoryDbContext<ApplicationDbContext>();
        this.Repository = this.Fixture.Create<GradingResultRepository>();
    }
}