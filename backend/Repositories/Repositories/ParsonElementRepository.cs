using Common.Models.ExerciseSystem.Parson;
using Common.RepositoryInterfaces.Tables;
using Repositories.Repositories.GenericImplementations;

namespace Repositories.Repositories;

public class ParsonElementRepository: GenericCrudRepository<ParsonElement>, IParsonElementRepository
{
    public ParsonElementRepository(ApplicationDbContext context) : base(context)
    {
    }
}