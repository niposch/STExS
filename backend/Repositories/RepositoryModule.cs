using Autofac;
using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Generic;
using Common.RepositoryInterfaces.Tables;
using Repositories.Repositories;
using Module = Autofac.Module;

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
        builder.RegisterType<ParsonElementRepository>()
            .As<IParsonElementRepository>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ParsonExerciseRepository>()
            .As<IParsonExerciseRepository>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ParsonSolutionRepository>()
            .As<IParsonSolutionRepository>();
        builder.RegisterType<ModuleRepository>()
            .As<IModuleRepository>()
            .InstancePerLifetimeScope();
        RegisterRepositories(builder);
        builder.RegisterType<ChapterRepository>()
            .As<IChapterRepository>()
            .InstancePerLifetimeScope();
        builder.RegisterType<CommonExerciseRepository>()
            .As<ICommonExerciseRepository>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ModuleParticipationRepository>()
            .As<IModuleParticipationRepository>()
            .InstancePerLifetimeScope();

        
    }

    private void RegisterRepositories(ContainerBuilder builder)
    {
        builder.RegisterType<WeatherForecastRepository>()
            .As<IWeatherForecastRepository>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ParsonExerciseRepository>()
            .As<IParsonExerciseRepository>()
            .InstancePerLifetimeScope();
    }
}