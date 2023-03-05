using Microsoft.AspNetCore.Mvc;
using STExS.Controllers.Submission;
using Microsoft.AspNetCore.Authorization;

namespace STExS;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ParsonPuzzleSubmissionController: ControllerBase,
ISubmissionController<ParsonPuzzleSubmissionCreateItem, ParsonPuzzleSubmissionDetailItem>
{

    #region User Routes

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Route("submit/{timeTrackId:guid}")]
    public async Task<IActionResult> SubmitSubmission([FromBody] ParsonPuzzleSubmissionCreateItem createItem, Guid timeTrackId, [FromQuery]bool isFinalSubmission, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ParsonPuzzleSubmissionDetailItem))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Route("get/{parsonPuzzleExerciseId:guid}")]
    public async Task<IActionResult> TryGetLastSubmission([FromRoute]Guid parsonPuzzleExerciseId, [FromQuery]Guid? currentTimeTrackId = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    #endregion

    #region Admin Routes


    #endregion
}

public class ParsonPuzzleSubmissionDetailItem: ParsonPuzzleSubmissionCreateItem
{
    public DateTime SubmittedAt { get; set; }
}

public class ParsonPuzzleSubmissionCreateItem
{
    public List<string> SubmittedAnswers { get; set; } = new();
    public Guid ExerciseId { get; set; }
}
