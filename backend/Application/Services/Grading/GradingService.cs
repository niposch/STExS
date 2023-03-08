using System.Runtime.CompilerServices;
using Application.DTOs;
using Application.DTOs.ChapterDTOs;
using Application.DTOs.ExercisesDTOs;
using Application.DTOs.GradingReportDTOs;
using Application.DTOs.ModuleDTOs;
using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.CodeOutput;
using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services.Grading;

public sealed class GradingService : IGradingService
{
    private readonly IApplicationRepository applicationRepository;

    private readonly ICodeOutputGradingService codeOutputGradingService;

    private readonly IAccessService accessService;

    public GradingService(IApplicationRepository applicationRepository,
        ICodeOutputGradingService codeOutputGradingService,
        IAccessService accessService)
    {
        this.applicationRepository = applicationRepository;
        this.codeOutputGradingService = codeOutputGradingService ?? throw new ArgumentNullException(nameof(codeOutputGradingService));
        this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
    }

    public async Task RunAutomaticGradingForExerciseAsync(BaseSubmission submission)
    {
        if (submission is CodeOutputSubmission codeOutputSubmission)
        {
            await this.codeOutputGradingService.GradeAsync(codeOutputSubmission);
        }
    }

    public async Task ManuallyGradeExerciseAsync(Guid submissionId,
        int newGrade,
        string comment,
        Guid changedByUserId,
        CancellationToken cancellationToken = default)
    {
        var submission = await this.applicationRepository.Submissions.TryGetByIdAsync(submissionId, cancellationToken);
        if (submission == null)
        {
            throw new EntityNotFoundException<BaseSubmission>(submissionId);
        }

        if (!await this.accessService.IsModuleAdmin(submission.UserSubmission.Exercise.Chapter.Module.Id, changedByUserId, cancellationToken))
        {
            throw new UnauthorizedAccessException();
        }

        if (submission.GradingResult == null)
        {
            submission.GradingResult = new GradingResult
            {
                Id = Guid.NewGuid(),
                AppealDate = null,
                GradedSubmissionId = submission.Id,
            };
        }

        submission.GradingResult.Points = newGrade;
        submission.GradingResult.Comment = comment;
        submission.GradingResult.ManualGradingDate = DateTime.Now;
        submission.GradingResult.GradingState = GradingState.FinallyManuallyGraded;
        submission.GradingResult.IsAutomaticallyGraded = false;
        submission.GradingResult.AppealableBefore = null;
        await this.applicationRepository.GradingResults.UpdateAsync(submission.GradingResult, cancellationToken);
    }

    public async Task<List<ExerciseReportItem>> GetExerciseReportAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var exercise = await this.applicationRepository.CommonExercises.TryGetByIdAsync(exerciseId, false, cancellationToken);

        if (exercise == null)
        {
            throw new EntityNotFoundException<BaseExercise>(exerciseId);
        }

        var chapter = await this.applicationRepository.Chapters.TryGetByIdAsync(exercise.ChapterId, cancellationToken);
        if (chapter == null)
        {
            throw new EntityNotFoundException<Chapter>(exercise.ChapterId);
        }

        var reportItems = new List<ExerciseReportItem>();

        var moduleParticipations = await this.applicationRepository.ModuleParticipations.GetParticipationsForModuleAsync(chapter.ModuleId, cancellationToken);
        var allSubmissions = await this.applicationRepository.UserSubmissions.GetAllByExerciseIdGroupedByUserIdAsync(exerciseId, cancellationToken);

        foreach (var participation in moduleParticipations)
        {
            allSubmissions.TryGetValue(participation.UserId, out var userSubmission);
            BaseSubmission? lastSubmission = null;
            var lastSubmissionState = SubmissionState.NotStarted;
            var isFinalSubmission = false;
            if (userSubmission != null)
            {
                if (userSubmission.FinalSubmission == null)
                {
                    lastSubmission = userSubmission.Submissions
                                     .OrderByDescending(s => s.CreationTime).FirstOrDefault();

                    isFinalSubmission = false;
                    lastSubmissionState = lastSubmission == null ? SubmissionState.StartedButNothingSubmitted : SubmissionState.TemporarySubmitted;
                }
                else
                {
                    isFinalSubmission = true;
                    lastSubmission = userSubmission.FinalSubmission;
                    lastSubmissionState = SubmissionState.FinalSolutionSubmitted;
                }
            }
            var lastSubmissionGradingState = SubmissionGradingState.NotGraded;
            if (lastSubmission?.GradingResult != null)
            {
                lastSubmissionGradingState = lastSubmission.GradingResult.IsAutomaticallyGraded ? SubmissionGradingState.AutomaticGraded : SubmissionGradingState.ManuallyGraded;
            }

            var item = new ExerciseReportItem
            {
                ExerciseName = exercise.ExerciseName,
                ExerciseId = exerciseId,
                UserId = participation.UserId,
                UserName = participation.User.UserName,
                UserFirstName = participation.User.FirstName,
                UserEmail = participation.User.Email,
                UserLastName = participation.User.LastName,
                ExerciseType = exercise.ExerciseType,
                MatrikelNumber = participation.User.MatrikelNumber,
                LastIsFinalSubmission = userSubmission == null ? null : userSubmission.FinalSubmission != null,
                LastComment = userSubmission?.FinalSubmission?.GradingResult?.Comment,
                LastPoints = lastSubmission?.GradingResult?.Points,
                LastGradingTime = lastSubmission?.GradingResult?.CreationDate,
                LastIsAutomatic = lastSubmission?.GradingResult?.IsAutomaticallyGraded,
                LastSubmissionId = lastSubmission?.Id,
                LastSubmissionTime = lastSubmission?.CreationTime,
                IsFinalSubmission = isFinalSubmission,
                LastSubmissionState = lastSubmissionState,
                LastSubmissionGradingState = lastSubmissionGradingState
            };

            reportItems.Add(item);
        }

        return reportItems;
    }

    public async Task<ModuleReport> GetModuleReportAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
        var module = await this.applicationRepository.Modules.TryGetByIdAndIncludeChapterExercisesAndUserSubmissionsAsync(moduleId, cancellationToken);
        if (module == null)
        {
            throw new EntityNotFoundException<Module>(moduleId);
        }

        var allUserIds = (await this.applicationRepository.ModuleParticipations.GetParticipationsForModuleAsync(moduleId, cancellationToken))
            .Where(p => p.ParticipationConfirmed)
            .Select(p => p.UserId)
            .ToList();
        var dictionary = await GetModuleAndRelatedDataDictionary(module, cancellationToken);
        var moduleReportItem = this.GenerateModuleReportItem(module, allUserIds, dictionary);

        return moduleReportItem;
    }

    private async Task<Dictionary<Guid/*ChapterId*/, Dictionary<Guid /*exerciseId*/, Dictionary<Guid/*UserId*/, UserSubmission>>>> GetModuleAndRelatedDataDictionary(Module module, CancellationToken cancellationToken = default)
    {
        var dict = new Dictionary<Guid, Dictionary<Guid, Dictionary<Guid, UserSubmission>>>();
        foreach (var chapter in module.Chapters)
        {
            var chapterDict = new Dictionary<Guid, Dictionary<Guid, UserSubmission>>();
            foreach (var exercise in chapter.Exercises)
            {
                var exerciseDict = new Dictionary<Guid, UserSubmission>();
                var submissions = await this.applicationRepository.UserSubmissions.GetAllByExerciseIdGroupedByUserIdAsync(exercise.Id, cancellationToken);
                foreach (var submission in submissions)
                {
                    exerciseDict[submission.Key] = submission.Value;
                }

                chapterDict[exercise.Id] = exerciseDict;
            }

            dict[chapter.Id] = chapterDict;
        }

        return dict;
    }

    private ModuleReport GenerateModuleReportItem(Module module, List<Guid> allUserIds,
        Dictionary<Guid/*ChapterId*/, Dictionary<Guid /*exerciseId*/, Dictionary<Guid/*UserId*/, UserSubmission>>> allSubmissions)
    {
        var chapterReports = new List<ChapterReport>();
        foreach (var chapter in module.Chapters)
        {
            var chapterReport = this.GenerateChapterReportItem(
                chapter,
                allUserIds,
                allSubmissions.GetValueOrDefault(chapter.Id, new Dictionary<Guid, Dictionary<Guid, UserSubmission>>()));
            chapterReports.Add(chapterReport);
        }

        Dictionary<Guid, int> userPoints = new Dictionary<Guid, int>();

        double averageTime = 0;

        // calculate the total points for each user and store them in the dictionary
        foreach (var chapterReport in chapterReports)
        {
            foreach (var userId in allUserIds)
            {
                var userReports = chapterReport
                    .Exercises
                    .SelectMany(e => e.Distribution.UserPoints)
                    .Where(u => u.UserId == userId)
                    .ToList();

                if (userPoints.ContainsKey(userId))
                {
                    userPoints[userId] += userReports.Sum(u => u.TotalPoints);
                }
                else
                {
                    userPoints[userId] = userReports.Sum(u => u.TotalPoints);
                }
            }
            averageTime += chapterReport.AverageTimeInMilliseconds;
        }

        var pointDistribution = new PointDistribution
        {
            UserPoints = userPoints.Select(u => new UserPoints
            {
                UserId = u.Key,
                TotalPoints = u.Value
            }).ToList()
        };


        var median = pointDistribution.UserPoints
            .Select(u => u.TotalPoints)
            .OrderBy(u => u)
            .Skip(pointDistribution.UserPoints.Count / 2)
            .FirstOrDefault(0);

        return new ModuleReport
        {
            AverageScore = pointDistribution.UserPoints
                .DefaultIfEmpty(new UserPoints { TotalPoints = 0 })
                .Average(u => u.TotalPoints),
            MedianScore = median,
            Chapters = chapterReports
                .OrderBy(c => c.Chapter.RunningNumber)
                .ToList(),
            Module = ModuleDetailItem.ToDetailItem(module),
            Distribution = pointDistribution,
            AverageTimeInMilliseconds = averageTime
        };
    }

    private ChapterReport GenerateChapterReportItem(
        Chapter c,
        List<Guid> allUserIds,
        Dictionary<Guid /*exerciseId*/, Dictionary<Guid /*userId*/, UserSubmission>> submissions)
    {
        var exerciseReports = c.Exercises
            .Select(e =>
                this.GenerateExerciseReportItem(e, allUserIds, submissions.GetValueOrDefault(e.Id, new Dictionary<Guid, UserSubmission>())))
            .ToDictionary(e => e.Exercise.ExerciseId);
        if (exerciseReports == null)
        {
            throw new Exception("exerciseReports is null");
        }

        Dictionary<Guid, int> userPoints = new Dictionary<Guid, int>();

        double averageTime = 0;

        foreach (var exerciseReport in exerciseReports.Values)
        {
            foreach (var userPointsItem in exerciseReport.Distribution.UserPoints)
            {
                if (!userPoints.ContainsKey(userPointsItem.UserId))
                {
                    userPoints[userPointsItem.UserId] = 0;
                }

                userPoints[userPointsItem.UserId] += userPointsItem.TotalPoints;
            }

            averageTime += exerciseReport.AverageTimeInMilliseconds;
        }

        var pointDistribution = new PointDistribution
        {
            UserPoints = userPoints.Select(u => new UserPoints
            {
                UserId = u.Key,
                TotalPoints = u.Value
            }).ToList()
        };

        var median = pointDistribution.UserPoints
            .Select(u => u.TotalPoints)
            .OrderBy(u => u)
            .Skip(pointDistribution.UserPoints.Count / 2)
            .FirstOrDefault(0);

        return new ChapterReport
        {
            AverageScore = pointDistribution.UserPoints
                .DefaultIfEmpty(new UserPoints { TotalPoints = 0 })
                .Average(u => u.TotalPoints),
            MedianScore = median,
            Chapter = ChapterDetailItem.ToDetailItem(c),
            Exercises = exerciseReports.Values
                .OrderBy(e => e.Exercise.RunningNumber)
                .ToList(),
            Distribution = pointDistribution,
            AverageTimeInMilliseconds = averageTime
        };
    }

    private ExerciseReport GenerateExerciseReportItem(BaseExercise exercise, List<Guid> allUserIds, Dictionary<Guid /*userId*/, UserSubmission> submissions)
    {

        var distribution = this.GeneratePointDistributionForExercise(submissions, allUserIds);

        var times = this.GenerateProcessingTimeForExercise(submissions, allUserIds);

        var median = distribution.UserPoints
            .Select(u => u.TotalPoints)
            .OrderBy(u => u)
            .Skip(distribution.UserPoints.Count / 2)
            .FirstOrDefault(0);

        var averageTime = times.UserTimes
            .Where(t => t.TotalTime != TimeSpan.Zero)
            .DefaultIfEmpty(new UserTime { TotalTime = TimeSpan.Zero })
            .Average(t => t.TotalTime.TotalMilliseconds);

        return new ExerciseReport
        {
            AverageScore = distribution.UserPoints
                .DefaultIfEmpty(new UserPoints { TotalPoints = 0 })
                .Average(u => u.TotalPoints),
            MedianScore = median,
            Exercise = ExerciseListItem.ToListItem(exercise),
            Distribution = distribution,
            AverageTimeInMilliseconds = averageTime
        };
    }

    private ProcessingTimeDistribution GenerateProcessingTimeForExercise(Dictionary<Guid, UserSubmission> submissions, List<Guid> allUserIds)
    {
        var times = new ProcessingTimeDistribution
        {
            UserTimes = new List<UserTime>()
        };
        foreach (var userId in allUserIds)
        {
            if (submissions.TryGetValue(userId, out var submission))
            {
                TimeSpan totalTime = TimeSpan.Zero;
                if (submission.FinalSubmission != null)
                {
                    submission.TimeTracks.ForEach(t => totalTime.Add(t.CloseDateTime?.Subtract(t.Start) ?? t.LastUpdate?.Subtract(t.Start) ?? TimeSpan.Zero));
                }
                times.UserTimes.Add(new UserTime
                {
                    UserId = userId,
                    TotalTime = totalTime
                });
            }
            else
            {
                times.UserTimes.Add(new UserTime()
                {
                    UserId = userId,
                    TotalTime = TimeSpan.Zero
                });
            }
        }

        return times;
    }
    private PointDistribution GeneratePointDistributionForExercise(Dictionary<Guid, UserSubmission> submissions, List<Guid> allUserIds)
    {
        var distribution = new PointDistribution
        {
            UserPoints = new List<UserPoints>()
        };

        foreach (var userId in allUserIds)
        {
            if (submissions.TryGetValue(userId, out var submission))
            {
                distribution.UserPoints.Add(new UserPoints
                {
                    UserId = userId,
                    TotalPoints = submission.FinalSubmission?.GradingResult?.Points ?? 0
                });
            }
            else
            {
                distribution.UserPoints.Add(new UserPoints
                {
                    UserId = userId,
                    TotalPoints = 0
                });
            }
        }

        return distribution;
    }
}

internal static class DictionaryExtensions
{
    internal static TValue? GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey? key, TValue? defaultValue = null)
    where TKey : notnull
    where TValue : struct
    {
        if (key == null)
        {
            return defaultValue;
        }
        if (dict.TryGetValue(key, out var value))
        {
            return value;
        }
        return defaultValue;
    }
}
