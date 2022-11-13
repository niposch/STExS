using System.Text;
using Application;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configure AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(b =>
{
    b.RegisterModule<ApplicationModule>();
    b.RegisterModule(new RepositoryModule(builder.Configuration.GetConnectionString("ApplicationDb"),
        builder.Environment.IsDevelopment()));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();
builder.Services.AddRazorPages();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
    app.UseDeveloperExceptionPage();
    var userManager = app.Services.GetRequiredService<UserManager<ApplicationUser>>();
    var user = userManager.FindByNameAsync("dev").Result;
    if (user != null) userManager.DeleteAsync(user).Wait();
    user = new ApplicationUser
    {
        UserName = "dev",
        Email = "dev@test.com"
    };
    userManager.CreateAsync(user, "Test333!").Wait();
    var token = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
    userManager.ConfirmEmailAsync(user, token).Wait();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseStaticFiles();
app.MapRazorPages();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();