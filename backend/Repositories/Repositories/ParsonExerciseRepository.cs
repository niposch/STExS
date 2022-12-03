using Application.Interfaces.Repositories.Tables;
using Common.Models.ExerciseSystem.Parson;
using Repositories.Repositories.GenericImplementations;

namespace Repositories.Repositories;

public class ParsonExerciseRepository: GenericCrudRepository<ParsonExercise>, IParsonExerciseRepository
{
    public ParsonExerciseRepository(ApplicationDbContext context) : base(context)
    {
    }
}