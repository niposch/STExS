using Application.Interfaces.Repositories.Tables;
using Application.Services.Interfaces;
using Common.Models.ExerciseSystem.Parson;

namespace Application.Services;

public class ParsonPuzzleService:IParsonPuzzleService
{
    private readonly IParsonExerciseRepository parsonRepository;

    public ParsonPuzzleService(IParsonExerciseRepository parsonRepository)
    {
        this.parsonRepository = parsonRepository ?? throw new ArgumentNullException(nameof(parsonRepository));
    }

    public async Task<List<ParsonExercise>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await this.parsonRepository.GetAllAsync();
    }
}