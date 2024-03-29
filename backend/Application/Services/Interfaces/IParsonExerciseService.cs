﻿using Application.DTOs.ExercisesDTOs;
using Application.DTOs.ExercisesDTOs.Parson;

namespace Application.Services.Interfaces;

public interface IParsonExerciseService
{
    public Task<ParsonExerciseDetailItem> GetByIdAsync(Guid exerciseId, Guid userId, CancellationToken cancellationToken = default);

    public Task<ParsonExerciseDetailItemWithAnswer> GetByIdWithAnswerAsync(Guid exerciseId, Guid userId, CancellationToken cancellationToken = default);

    public Task UpdateAsync(ParsonExerciseDetailItemWithAnswer updateItem, CancellationToken cancellationToken = default);

    public Task<Guid> CreateAsync(ParsonExerciseCreateItem createItem, Guid userId, CancellationToken cancellationToken = default);
}

public class ParsonExerciseDetailItemWithAnswer : ExerciseDetailItem
{
    public List<ParsonExerciseLineDetailItem> Lines { get; set; }
    public bool IndentationIsRelevant { get; set; }
}