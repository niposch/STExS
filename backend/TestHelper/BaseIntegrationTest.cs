using AutoFixture;
using AutoFixture.AutoMoq;
using Common.Models;
using Kralizek.AutoFixture.Extensions.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repositories;

namespace TestHelper;

public abstract class BaseIntegrationTest
{
    protected readonly HttpClient Client;

    protected readonly IFixture Fixture;

    protected readonly ApplicationDbContext Context;
    
    protected IServiceProvider Services;

    protected ApplicationUser DefaultUser;
    
    protected BaseIntegrationTest()
    {
        Fixture = new Fixture().Customize(new AutoMoqCustomization());
        Fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        var builder = new WebApplicationFactory<Program>();
        
        // configure context to use in-memory database
        builder.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                services.Remove(descriptor);
                
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();
                
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlite(connection);
                    options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()));
                });
            });
        });
        
        this.Context = builder.Services.GetRequiredService<ApplicationDbContext>();
        this.Services = builder.Services;
        this.Context.Database.EnsureCreated();
        
        CreateDefaultUser();
        Client = builder.CreateClient();
    }


    protected void CreateDefaultUser()
    {
        var userManager = this.Services.GetRequiredService<UserManager<ApplicationUser>>();
        this.DefaultUser = new ApplicationUser
        {
            UserName = "test",
            Email = "test@test.com",
            LockoutEnabled = false
        };
        // disable lockout for the user
        userManager.CreateAsync(this.DefaultUser, "Test333!").Wait();
        this.DefaultUser = userManager.FindByNameAsync("test").Result;
        var token = userManager.GenerateEmailConfirmationTokenAsync(this.DefaultUser).Result;
        userManager.ConfirmEmailAsync(this.DefaultUser, token).Wait();
        this.DefaultUser = this.DefaultUser;
    }
}