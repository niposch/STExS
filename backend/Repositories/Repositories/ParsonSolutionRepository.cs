using Common.Models.ExerciseSystem.Parson;
using Common.RepositoryInterfaces.Tables;
using Repositories.Repositories.GenericImplementations;

namespace Repositories.Repositories;

public class ParsonSolutionRepository: GenericCrudRepository<ParsonSolution>, IParsonSolutionRepository
{
    public ParsonSolutionRepository(ApplicationDbContext context) : base(context)
    {
    }
}