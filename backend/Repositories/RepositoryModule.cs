using Autofac;
using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Generic;
using Common.RepositoryInterfaces.Tables;
using Module = Autofac.Module;

namespace Repositories.Repositories.Grading;

public class RepositoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ApplicationDbContext>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ApplicationRepository>()
            .As<IApplicationRepository>()
            .InstancePerLifetimeScope();
        this.RegisterRepositories(builder);
    }

    private void RegisterRepositories(ContainerBuilder builder)
    {
        builder.RegisterType<ParsonExerciseRepository>()
            .As<IParsonExerciseRepository>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ParsonElementRepository>()
            .As<IParsonElementRepository>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ParsonExerciseRepository>()
            .As<IParsonExerciseRepository>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ParsonSolutionRepository>()
            .As<IParsonSolutionRepository>();

        builder.RegisterType<CodeOutputExerciseRepository>()
            .As<ICodeOutputExerciseRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ClozeTextExerciseRepository>()
            .As<IClozeTextExerciseRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CommonExerciseRepository>()
            .As<ICommonExerciseRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ModuleRepository>()
            .As<IModuleRepository>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ChapterRepository>()
            .As<IChapterRepository>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ModuleParticipationRepository>()
            .As<IModuleParticipationRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<UserSubmissionRepository>()
            .As<IUSerSubmissionRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<SubmissionRepository>()
            .As<ISubmissionRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<GradingResultRepository>()
            .As<IGradingResultRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<TimeTrackRepository>()
            .As<ITimeTrackRepository>()
            .InstancePerLifetimeScope();
    }
}