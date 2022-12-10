using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Tables;
using Repositories.Repositories.GenericImplementations;

namespace Repositories.Repositories;

public class ChapterRepository: GenericCrudRepository<Chapter>, IChapterRepository
{
    public ChapterRepository(ApplicationDbContext context) : base(context)
    {
    }
}