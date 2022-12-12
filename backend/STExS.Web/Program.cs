using System.Net;
using System.Text;
using Application;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpLogging(options => { options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders; });

// Configure AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
var isTest = builder.Configuration.GetValue("IsTest", false);
var isDevelopment = builder.Environment.IsDevelopment();
builder.Host.ConfigureContainer<ContainerBuilder>(b =>
{
    var connectionString = builder.Configuration.GetConnectionString("ApplicationDb");
    if (!isTest) // test environment should provide it's own db options before this point
    {
        b.Register<DbContextOptions<ApplicationDbContext>>(_ =>
            new DbContextOptionsFactory<ApplicationDbContext>(connectionString, isDevelopment).CreateOptions());
    }
    b.RegisterModule<ApplicationModule>();
    b.RegisterModule(new RepositoryModule());
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.All;
    // Only loopback proxies are allowed by default.
    // Clear that restriction because forwarders are enabled by explicit 
    // configuration.
    options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("172.0.0.0"), 8));
    options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("127.0.0.1"), 0));
    // options.KnownProxies.Add(IPAddress.Parse(""));
});

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.ConfigureApplicationCookie(o =>
{
    o.Cookie.SameSite = SameSiteMode.None;
    o.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseForwardedHeaders();
app.UseHttpLogging();

app.Use(async (context,
    next) =>
{
    // Connection: RemoteIp
    app.Logger.LogInformation("Request RemoteIp: {RemoteIpAddress}",
        context.Connection.RemoteIpAddress);

    await next(context);
});

if (!isTest)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "api/swagger";
        c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "Application");
    });
    app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
    app.UseDeveloperExceptionPage();
    var userManager = app.Services.GetRequiredService<UserManager<ApplicationUser>>();
    var user = userManager.FindByNameAsync("dev").Result;
    if (user == null)
    {
        user = new ApplicationUser
        {
            UserName = "dev",
            Email = "dev@test.com",
            FirstName = "John",
            LastName = "Doe",
            MatrikelNumber = "J1234567"
        };
        userManager.CreateAsync(user, "Test333!").Wait();
        user = userManager.FindByNameAsync("dev").Result;
        var token = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
        userManager.ConfirmEmailAsync(user, token).Wait();
    }
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();