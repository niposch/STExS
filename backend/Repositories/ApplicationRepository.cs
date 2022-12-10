using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Generic;
using Common.RepositoryInterfaces.Tables;

namespace Repositories;

public class ApplicationRepository : IApplicationRepository
{
    public ApplicationRepository(IWeatherForecastRepository weatherForecasts,
        IModuleRepository modules,
        IChapterRepository chapters,
        IParsonElementRepository parsonElements,
        IParsonExerciseRepository parsonExercises,
        IParsonSolutionRepository parsonSolutions,
        ICommonExerciseRepository commonExercises,
        IModuleParticipationRepository moduleParticipations)
    {
        WeatherForecasts = weatherForecasts ?? throw new ArgumentNullException(nameof(weatherForecasts));
        this.Modules = modules ?? throw new ArgumentNullException(nameof(modules));
        this.Chapters = chapters ?? throw new ArgumentNullException(nameof(chapters));
        this.ParsonElements = parsonElements ?? throw new ArgumentNullException(nameof(parsonElements));
        this.ParsonExercises = parsonExercises ?? throw new ArgumentNullException(nameof(parsonExercises));
        this.ParsonSolutions = parsonSolutions ?? throw new ArgumentNullException(nameof(parsonSolutions));
        this.CommonExercises = commonExercises ?? throw new ArgumentNullException(nameof(commonExercises));
        this.ModuleParticipations = moduleParticipations ?? throw new ArgumentNullException(nameof(moduleParticipations));
    }

    public IWeatherForecastRepository WeatherForecasts { get; set; } // just for demonstration, will be removed
    
    public IModuleRepository Modules { get; set; }
    public IChapterRepository Chapters { get; set; }
    
    // Exercises
    public ICommonExerciseRepository CommonExercises { get; set; } // helper repository for querying all exercises
    // Parson 
    public IParsonElementRepository ParsonElements { get; set; }
    public IParsonExerciseRepository ParsonExercises { get; set; }
    public IParsonSolutionRepository ParsonSolutions { get; set; }
    public IModuleParticipationRepository ModuleParticipations { get; set; }
}