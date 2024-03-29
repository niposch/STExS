﻿using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Generic;
using Common.RepositoryInterfaces.Tables;

namespace Repositories;

public class ApplicationRepository : IApplicationRepository
{
#pragma warning disable
    public ApplicationRepository(
        IModuleRepository modules,
        IChapterRepository chapters,
        IParsonElementRepository parsonElements,
        IParsonExerciseRepository parsonExercises,
        IParsonSolutionRepository parsonSolutions,
        ICommonExerciseRepository commonExercises,
        IModuleParticipationRepository moduleParticipations,
        ICodeOutputExerciseRepository codeOutputExercises,
        IUSerSubmissionRepository userSubmissions,
        ISubmissionRepository submissions,
        IGradingResultRepository gradingResults,
        ITimeTrackRepository timeTracks,
        IClozeTextExerciseRepository clozeTextExercises)
#pragma warning restore
    {
        this.Modules = modules ?? throw new ArgumentNullException(nameof(modules));
        this.Chapters = chapters ?? throw new ArgumentNullException(nameof(chapters));
        this.ParsonElements = parsonElements ?? throw new ArgumentNullException(nameof(parsonElements));
        this.ParsonExercises = parsonExercises ?? throw new ArgumentNullException(nameof(parsonExercises));
        this.ParsonSolutions = parsonSolutions ?? throw new ArgumentNullException(nameof(parsonSolutions));
        this.CommonExercises = commonExercises ?? throw new ArgumentNullException(nameof(commonExercises));
        this.ModuleParticipations = moduleParticipations ?? throw new ArgumentNullException(nameof(moduleParticipations));
        this.CodeOutputExercises = codeOutputExercises ?? throw new ArgumentNullException(nameof(codeOutputExercises));
        this.ClozeTextExercises = clozeTextExercises ?? throw new ArgumentNullException(nameof(clozeTextExercises));

        this.UserSubmissions = userSubmissions ?? throw new ArgumentNullException(nameof(userSubmissions));
        this.Submissions = submissions ?? throw new ArgumentNullException(nameof(submissions));
        this.GradingResults = gradingResults ?? throw new ArgumentNullException(nameof(gradingResults));
        this.TimeTracks = timeTracks ?? throw new ArgumentNullException(nameof(timeTracks));
    }

    public IModuleRepository Modules { get; set; }
    public IChapterRepository Chapters { get; set; }

    // Exercises
    public ICommonExerciseRepository CommonExercises { get; set; } // helper repository for querying all exercises

    // Parson 
    public IParsonElementRepository ParsonElements { get; set; }
    public IParsonExerciseRepository ParsonExercises { get; set; }
    public IParsonSolutionRepository ParsonSolutions { get; set; }
    public ICodeOutputExerciseRepository CodeOutputExercises { get; set; }

    public IClozeTextExerciseRepository ClozeTextExercises { get; set; }
    public IModuleParticipationRepository ModuleParticipations { get; set; }

    #region Grading

    public IUSerSubmissionRepository UserSubmissions { get; set; }

    public ISubmissionRepository Submissions { get; set; }

    public IGradingResultRepository GradingResults { get; set; }

    public ITimeTrackRepository TimeTracks { get; set; }

    #endregion
}