using Common.Models.ExerciseSystem.Parson;

namespace Application.Services.Interfaces;

public interface IParsonPuzzleService
{
    public Task<List<ParsonExercise>> GetAllAsync(CancellationToken cancellationToken = default);
}