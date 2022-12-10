using Application.DTOs.Exercises;
using Common.Models.ExerciseSystem;

namespace Application.Services.Interfaces;

public interface IExerciseService
{
    public Task<List<Tuple<BaseExercise, ExerciseType>>> GetExercisesForChapterAsync(Guid chapterId, CancellationToken cancellationToken = default);
}