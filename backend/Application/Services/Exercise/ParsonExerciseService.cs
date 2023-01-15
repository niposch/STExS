using Application.Services.Interfaces;
using Common.RepositoryInterfaces.Tables;

namespace Application.Services.Exercise;

public class ParsonExerciseService:IParsonExerciseService
{
    private readonly IParsonExerciseRepository parsonRepository;

    public ParsonExerciseService(IParsonExerciseRepository parsonRepository)
    {
        this.parsonRepository = parsonRepository ?? throw new ArgumentNullException(nameof(parsonRepository));
    }
}