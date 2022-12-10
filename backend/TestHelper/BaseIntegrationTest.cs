using AutoFixture;
using AutoFixture.AutoMoq;
using Common.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repositories;

namespace TestHelper;

public abstract class BaseIntegrationTest
{
    protected readonly HttpClient Client;

    protected readonly ApplicationDbContext Context;

    protected readonly IFixture Fixture;

    protected IServiceProvider Services;

    protected BaseIntegrationTest()
    {
        Fixture = new Fixture().Customize(new AutoMoqCustomization());
        Fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var builder = new CustomWebApplicationFactory<Program>();

        // configure context to use in-memory database
        builder.WithWebHostBuilder(builder =>
        {

        });

        Context = builder.Services.GetRequiredService<ApplicationDbContext>();
        Services = builder.Services;
        Context.Database.EnsureCreated();

        Client = builder.CreateClient();
    }
}

internal class CustomWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        // override DbContextOptions registered in autofac

        builder.ConfigureHostConfiguration(c =>
        {
            c.SetBasePath(Directory.GetCurrentDirectory());
            c.AddJsonFile("appsettings.test.json", optional: false);
            c.AddEnvironmentVariables();
        });
        
        builder.ConfigureServices(services =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            services.AddSingleton(optionsBuilder.Options);
        });
        
        return base.CreateHost(builder);
    }
}