using Common.Models.ExerciseSystem.Cloze;
using Common.RepositoryInterfaces.Tables;
using Repositories.Repositories.GenericImplementations;

namespace Repositories.Repositories;

public class ClozeTextExerciseRepository : GenericCrudRepository<ClozeTextExercise>, IClozeTextExerciseRepository
{
    public ClozeTextExerciseRepository(ApplicationDbContext context) : base(context)
    {
    }
}