using Common.Models.ExerciseSystem;
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

    public async Task<CodeOutputExercise> UpdateAsync(CodeOutputExercise exercise, CancellationToken cancellationToken = default)
    {
        this.context.RemoveLocalIfTracked(exercise);
        this.context.CodeOutputExercises.Update(exercise);
        await this.context.SaveChangesAsync(cancellationToken);
        
        return exercise;
    }

    public async Task<CodeOutputExercise> CreateAsync(CodeOutputExercise entity, CancellationToken cancellationToken = default)
    {
        this.context.RemoveLocalIfTracked(entity);
        await this.context.CodeOutputExercises.AddAsync(entity, cancellationToken);
        await this.context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}