using Application.Interfaces.Repositories.Tables;
using Application.Services;
using Application.Services.Interfaces;
using Autofac;

namespace Application;

public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<WeatherService>()
            .As<IWeatherService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ParsonPuzzleService>()
            .As<IParsonPuzzleService>()
            .InstancePerLifetimeScope();
    }
}