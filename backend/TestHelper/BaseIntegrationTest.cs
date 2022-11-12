using System.Runtime.CompilerServices;
using Autofac.Extensions.DependencyInjection;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;

namespace TestHelper;
public abstract class BaseIntegrationTest
{
    protected HttpClient Client { get; private set; }

    protected IFixture Fixture { get; private set; }

    protected BaseIntegrationTest()
    {
        this.Fixture = new Fixture().Customize(new AutoMoqCustomization());
        this.Fixture.Behaviors.AddServiceProvider();
        this.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        this.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        this.Fixture.Register<ILogger>(this.Fixture.Create<ILogger>);
        
        this.Fixture.AddWebApplicationFactorySupport<Program>();
        this.Client = this.Fixture.Create<HttpClient>();
    }
}