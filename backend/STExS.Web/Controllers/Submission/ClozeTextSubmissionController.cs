using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.ExerciseSystem.Cloze;
using Common.Models.ExerciseSystem.CodeOutput;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STExS.Helper;

namespace STExS.Controllers.Submission;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ClozeTextSubmissionController: ControllerBase,
ISubmissionController<ClozeTextSubmissionCreateItem, ClozeTextSubmissionDetailItem>
{

    private readonly IClozeTextSubmissionService clozeTextSubmissionService;
    private readonly ISubmissionService submissionService;

    public ClozeTextSubmissionController(IClozeTextSubmissionService clozeTextSubmissionService, ISubmissionService submissionService1)
    {
        this.clozeTextSubmissionService = clozeTextSubmissionService ?? throw new ArgumentNullException(nameof(clozeTextSubmissionService));
        this.submissionService = submissionService1 ?? throw new ArgumentNullException(nameof(submissionService1));
    }

    #region User Routes

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Route("submit/{timeTrackId:guid}")]
    public async Task<IActionResult> SubmitSubmission([FromBody] ClozeTextSubmissionCreateItem createItem, Guid timeTrackId, [FromQuery]bool isFinalSubmission, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        try 
        {
            await this.clozeTextSubmissionService.SubmitAsync(userId, createItem.ExerciseId, isFinalSubmission, createItem.SubmittedAnswers, timeTrackId, cancellationToken);
        }
        catch (AlreadySubmittedException e)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        return this.Ok();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClozeTextSubmissionDetailItem))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Route("get/{clozeTextExerciseId:guid}")]
    public async Task<IActionResult> TryGetLastSubmission([FromRoute]Guid clozeTextExerciseId, [FromQuery]Guid? currentTimeTrackId = null, CancellationToken cancellationToken = default)
    {
    var userId = this.User.GetUserId();
        try
        {
            var submission = await this.submissionService.GetLastSubmissionForAnsweringAsync(userId, clozeTextExerciseId, currentTimeTrackId, cancellationToken);
            
            // if Submission is instance of ClozeTextSubmission
            if (submission is not ClozeTextSubmission clozeTextSubmission) 
                return this.NotFound();
            
            var submissionDetailItem = new ClozeTextSubmissionDetailItem
            {
                SubmittedAnswers = clozeTextSubmission
                    .SubmittedAnswers
                    .OrderBy(answer => answer.Index)
                    .Select(answer => answer.SubmittedAnswer)
                    .ToList(),
                SubmittedAt = clozeTextSubmission.CreationTime,
                ExerciseId = clozeTextSubmission.ExerciseId
            };
            return this.Ok(submissionDetailItem);
        }
        catch (AlreadySubmittedException e)
        {
            return this.Forbid();
        }
        catch (Exception e)
        {
            return this.NotFound();
        }
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