using Common.Models.ExerciseSystem.CodeOutput;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace STExS.Controllers.Submission;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CodeOutputSubmissionController: ControllerBase
{
    #region User Routes
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Route("submit/{timeTrackId:guid}")]
    public async Task<IActionResult> SubmitCodeOutputSubmission([FromBody] CodeOutputSubmissionCreateItem createItem, Guid timeTrackId, [FromQuery]bool isFinalSubmission, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CodeOutputSubmissionDetailItem))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Route("get/{codeOutputExerciseId:guid}")]
    public async Task<IActionResult> TryGetLastCodeOutputSubmission([FromRoute]Guid codeOutputExerciseId, [FromQuery]Guid currentTimeTrackId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    #endregion

    #region Admin Routes


    #endregion
}

public class CodeOutputSubmissionDetailItem
{
    public string SubmittedAnswer { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
}

public class CodeOutputSubmissionCreateItem
{
    public string SubmittedAnswer { get; set; } = string.Empty;
    public Guid ExerciseId { get; set; }
}