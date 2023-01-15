﻿using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Tables;

namespace Common.RepositoryInterfaces.Generic;

public interface IApplicationRepository
{
    public IWeatherForecastRepository WeatherForecasts { get; }
    IModuleRepository Modules { get; set; }
    IChapterRepository Chapters { get; set; }
    ICommonExerciseRepository CommonExercises { get; set; } // helper repository for querying all exercises
    
    IParsonElementRepository ParsonElements { get; set; }
    IParsonExerciseRepository ParsonExercises { get; set; }
    IParsonSolutionRepository ParsonSolutions { get; set; }
    
    
    ICodeOutputExerciseRepository CodeOutputExercises { get; set; }
    
    IModuleParticipationRepository ModuleParticipations { get; set; }
}