using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using Application;
using Application.Helper.Roles;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Models.Authentication;
using External.Judge0;
using External.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories;
using Repositories.Repositories.Grading;
using STExS.Helper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpLogging(options => { options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders; });

// Configure AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
var isTest = builder.Configuration.GetValue("IsTest", false);
var isCI = builder.Configuration.GetValue("IsCI", true); // true when run through swagger cli tool
var isDevelopment = builder.Environment.IsDevelopment();
builder.Host.ConfigureContainer<ContainerBuilder>(b =>
{
    var connectionString = builder.Configuration.GetConnectionString("ApplicationDb");
    if (!isTest && !isCI) // test environment should provide it's own db options before this point
    {
        b.Register<DbContextOptions<ApplicationDbContext>>(_ =>
            new DbContextOptionsFactory<ApplicationDbContext>(connectionString, isDevelopment).CreateOptions());
    }
    b.RegisterModule<ApplicationModule>();
    b.RegisterModule(new RepositoryModule());
    b.RegisterModule<StorageModule>();
    b.RegisterModule<Judge0Module>();
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
builder.Services.AddSwaggerGen(o =>
{
    o.SchemaFilter<EnumSchemaFilter>();
});

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

if (!isCI)
{
    
    if (!isTest)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    var roleManager = app.Services.GetRequiredService<RoleManager<ApplicationRole>>();
    var applicationRoles = new List<ApplicationRole>
    {
        new ApplicationRole {Name = RoleHelper.Admin},
        new ApplicationRole {Name = RoleHelper.User},
        new ApplicationRole {Name = RoleHelper.Teacher}
    };
    foreach (var role in applicationRoles)
    {
        if(roleManager.FindByNameAsync(role.Name).Result == null)
            roleManager.CreateAsync(role).Wait();
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
        var admin = userManager.FindByNameAsync("dev").Result;
        if (admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = "dev",
                Email = "dev@test.com",
                FirstName = "John",
                LastName = "Doe",
                MatrikelNumber = "J1234567"
            };
            userManager.CreateAsync(admin, "Test333!").Wait();
            admin = userManager.FindByNameAsync("dev").Result;
            var token = userManager.GenerateEmailConfirmationTokenAsync(admin).Result;
            userManager.ConfirmEmailAsync(admin, token).Wait();
        }
        userManager.AddToRoleAsync(admin, RoleHelper.Admin).Wait();
        userManager.AddToRoleAsync(admin, RoleHelper.Teacher).Wait();
        userManager.AddToRoleAsync(admin, RoleHelper.User).Wait();

        var teacher = userManager.FindByNameAsync("dev-teacher").Result;
        if (teacher == null)
        {
            teacher = new ApplicationUser
            {
                UserName = "dev-teacher",
                Email = "dev-teacher@test.com",
                FirstName = "Jeff",
                LastName = "Doe",
                MatrikelNumber = "ABC1234",
            };
            userManager.CreateAsync(teacher, "Test333!").Wait();
            teacher = userManager.FindByNameAsync("dev-teacher").Result;
            var token = userManager.GenerateEmailConfirmationTokenAsync(teacher).Result;
            userManager.ConfirmEmailAsync(teacher, token).Wait();
        }
        userManager.AddToRoleAsync(teacher, RoleHelper.Teacher).Wait();
        userManager.AddToRoleAsync(teacher, RoleHelper.User).Wait();
        
        var user = userManager.FindByNameAsync("dev-user").Result;
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = "dev-user",
                Email = "dev-user@test.com",
                FirstName = "Jane",
                LastName = "Doe",
                MatrikelNumber = "ABC1235",
            };
            userManager.CreateAsync(user, "Test333!").Wait();
            user = userManager.FindByNameAsync("dev-user").Result;
            var token = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            userManager.ConfirmEmailAsync(user, token).Wait();
        }
        userManager.AddToRoleAsync(user, RoleHelper.User).Wait();
    }
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();