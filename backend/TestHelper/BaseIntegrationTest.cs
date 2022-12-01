﻿using AutoFixture;
using AutoFixture.AutoMoq;
using Common.Models.Authentication;
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

    protected readonly ApplicationDbContext Context;

    protected readonly IFixture Fixture;

    protected ApplicationUser DefaultUser;

    protected IServiceProvider Services;

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
                var descriptor =
                    services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
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

        Context = builder.Services.GetRequiredService<ApplicationDbContext>();
        Services = builder.Services;
        Context.Database.EnsureCreated();

        CreateDefaultUser();
        Client = builder.CreateClient();
    }


    protected void CreateDefaultUser()
    {
        var userManager = Services.GetRequiredService<UserManager<ApplicationUser>>();
        DefaultUser = new ApplicationUser
        {
            UserName = "test",
            Email = "test@test.com",
            LockoutEnabled = false
        };
        // disable lockout for the user
        userManager.CreateAsync(DefaultUser, "Test333!").Wait();
        DefaultUser = userManager.FindByNameAsync("test").Result;
        var token = userManager.GenerateEmailConfirmationTokenAsync(DefaultUser).Result;
        userManager.ConfirmEmailAsync(DefaultUser, token).Wait();
    }
}