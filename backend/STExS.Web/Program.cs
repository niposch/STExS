using Application;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Models;
using Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Configure AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(b =>
{
    b.RegisterModule<ApplicationModule>();
    b.RegisterModule(new RepositoryModule(builder.Configuration.GetConnectionString("ApplicationDb"), builder.Environment.IsDevelopment()));
});

var sqliteConnectionBuilder = new SqliteConnectionStringBuilder();
sqliteConnectionBuilder.DataSource = "./IdentityDb.db";
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
}

app.UseHttpsRedirection();
app.UseAuthentication();;
app.UseStaticFiles();
app.MapRazorPages();

app.UseAuthorization();

app.MapControllers();

app.Run();