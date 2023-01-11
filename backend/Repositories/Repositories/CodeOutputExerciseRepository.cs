using Common.Models.ExerciseSystem.CodeOutput;
using Common.RepositoryInterfaces.Generic;
using Common.RepositoryInterfaces.Tables;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories;

public class CodeOutputExerciseRepository: ICodeOutputExerciseRepository
{
    private readonly ApplicationDbContext context;

    public CodeOutputExerciseRepository(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CodeOutputExercise?> TryGetById(Guid id, CancellationToken cancellationToken = default)
    {
        var ex = await this.context.CodeOutputExercises.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        return ex;
    }
}