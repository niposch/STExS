using Application.DTOs.Exercises;
using Application.Services.Interfaces;
using Common.Models.ExerciseSystem;

namespace Application.Services;

public sealed class ExerciseService: IExerciseService
{
    public async Task<List<Tuple<BaseExercise, ExerciseType>>> GetExercisesForChapterAsync(Guid chapterId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}