using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Tables;
using Autofac;
using Repositories.Repositories;

namespace Repositories;

public class RepositoryModule : Module
{
    private readonly string connectionString;
    private readonly bool isDevelopment;

    public RepositoryModule(string connectionString, bool isDevelopment)
    {
        this.connectionString = connectionString;
        this.isDevelopment = isDevelopment;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ApplicationDbContext>()
            .WithParameter("options",
                new DbContextOptionsFactory<ApplicationDbContext>(connectionString, isDevelopment).CreateOptions())
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