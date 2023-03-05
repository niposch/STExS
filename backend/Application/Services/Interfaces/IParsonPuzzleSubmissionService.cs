using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Interfaces;
public interface IParsonPuzzleSubmissionService
{
    public Task SubmitAsync(Guid userId,
        Guid exerciseId,
        bool isFinal,
        List<Guid> linesInSubmittedOrder,
        Guid timeTrackId,
        CancellationToken cancellationToken = default);
}