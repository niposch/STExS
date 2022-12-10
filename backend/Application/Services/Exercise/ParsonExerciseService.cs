using Application.Services.Interfaces;
using Common.Models.ExerciseSystem.Parson;
using Common.RepositoryInterfaces.Tables;

namespace Application.Services.Exercise;

public class ParsonExerciseService:IParsonPuzzleService
{
    private readonly IParsonExerciseRepository parsonRepository;

    public ParsonExerciseService(IParsonExerciseRepository parsonRepository)
    {
        this.parsonRepository = parsonRepository ?? throw new ArgumentNullException(nameof(parsonRepository));
    }

    public async Task<List<ParsonExercise>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await this.parsonRepository.GetAllAsync();
    }
}