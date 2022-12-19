using Application.Helper.Roles;
using Application.Interfaces;
using Application.Services;
using Application.Services.Exercise;
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

        builder.RegisterType<ParsonExerciseService>()
            .As<IParsonPuzzleService>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ChapterService>()
            .As<IChapterService>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ModuleService>()
            .As<IModuleService>()
            .InstancePerLifetimeScope();
        
        // Helper
        builder.RegisterType<RoleHelper>()
            .As<IRoleHelper>()
            .InstancePerLifetimeScope();

    }
}