using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace STExS.Controllers.Submission;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ClozeTextSubmissionController: ControllerBase,
ISubmissionController<ClozeTextSubmissionCreateItem, ClozeTextSubmissionDetailItem>
{

    #region User Routes

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Route("submit/{timeTrackId:guid}")]
    public async Task<IActionResult> SubmitSubmission([FromBody] ClozeTextSubmissionCreateItem createItem, Guid timeTrackId, [FromQuery]bool isFinalSubmission, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClozeTextSubmissionDetailItem))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Route("get/{clozeTextExerciseId:guid}")]
    public async Task<IActionResult> TryGetLastSubmission([FromRoute]Guid clozeTextExerciseId, [FromQuery]Guid? currentTimeTrackId = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    #endregion

    #region Admin Routes


    #endregion
}

public class ClozeTextSubmissionDetailItem: ClozeTextSubmissionCreateItem
{
    public DateTime SubmittedAt { get; set; }
}

public class ClozeTextSubmissionCreateItem
{
    public List<string> SubmittedAnswers { get; set; } = new();
    public Guid ExerciseId { get; set; }
}