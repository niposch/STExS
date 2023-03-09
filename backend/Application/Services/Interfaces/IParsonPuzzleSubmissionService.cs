using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.ExercisesDTOs.Parson;

namespace Application.Services.Interfaces;
public interface IParsonPuzzleSubmissionService
{
    public Task SubmitAsync(Guid userId,
        Guid exerciseId,
        bool isFinal,
        List<ParsonExerciseLineDetailItem> linesInSubmittedOrder,
        Guid timeTrackId,
        CancellationToken cancellationToken = default);
}