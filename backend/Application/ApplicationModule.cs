using Application.Helper;
using Application.Helper.Roles;
using Application.Interfaces;
using Application.Services;
using Application.Services.Exercise;
using Application.Services.Grading;
using Application.Services.Interfaces;
using Application.Services.Permissions;
using Autofac;

namespace Application;

public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<WeatherService>()
            .As<IWeatherService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ExerciseService>()
            .As<IExerciseService>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ParsonExerciseService>()
            .As<IParsonExerciseService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ClozeTextExerciseService>()
            .As<IClozeTextExerciseService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CodeOutputExerciseService>()
            .As<ICodeOutputExerciseService>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ChapterService>()
            .As<IChapterService>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ModuleService>()
            .As<IModuleService>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<EmailService>()
            .As<IEmailService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CodeOutputGradingService>()
            .As<ICodeOutputGradingService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<AccessService>()
            .As<IAccessService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<GradingService>()
            .As<IGradingService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<UserSubmissionService>()
            .As<IUserSubmissionService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<TimeTrackService>()
            .As<ITimeTrackService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<SubmissionService>()
            .As<ISubmissionService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CodeOutputSubmissionService>()
            .As<ICodeOutputSubmissionService>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<ParsonPuzzleSubmissionService>()
            .As<IParsonPuzzleSubmissionService>()
            .InstancePerLifetimeScope();

        // Helper
        builder.RegisterType<RoleHelper>()
            .As<IRoleHelper>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ClozeTextHelper>()
            .As<IClozeTextHelper>()
            .InstancePerLifetimeScope();
    }
}