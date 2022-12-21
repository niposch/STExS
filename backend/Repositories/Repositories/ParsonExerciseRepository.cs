using Common.Models.ExerciseSystem.Parson;
using Common.RepositoryInterfaces.Tables;
using Repositories.Repositories.GenericImplementations;

namespace Repositories.Repositories;

public class ParsonExerciseRepository: GenericCrudRepository<ParsonExercise>, IParsonExerciseRepository
{
    public ParsonExerciseRepository(ApplicationDbContext context) : base(context)
    {
    }
}