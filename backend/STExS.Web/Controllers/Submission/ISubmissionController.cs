using Microsoft.AspNetCore.Mvc;

namespace STExS.Controllers.Submission;
public interface ISubmissionController<TCreateItem, TDetailItem>
{
    public Task<IActionResult> SubmitSubmission([FromBody] TCreateItem createItem, Guid timeTrackId, [FromQuery]bool isFinalSubmission, CancellationToken cancellationToken = default);
    
    public Task<IActionResult> TryGetLastSubmission([FromRoute]Guid exerciseId, [FromQuery]Guid? currentTimeTrackId = null, CancellationToken cancellationToken = default);
}