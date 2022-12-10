using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Tables;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories.Repositories;

namespace Repositories;

public class RepositoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ApplicationDbContext>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ApplicationRepository>()
            .As<IApplicationRepository>()
            .InstancePerLifetimeScope();
        RegisterRepositories(builder);
        
    }

    private void RegisterRepositories(ContainerBuilder builder)
    {
        builder.RegisterType<WeatherForecastRepository>()
            .As<IWeatherForecastRepository>()
            .InstancePerLifetimeScope();
    }
}