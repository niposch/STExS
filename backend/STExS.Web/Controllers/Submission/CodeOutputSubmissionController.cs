using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.ExerciseSystem.CodeOutput;
using Common.Models.Grading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STExS.Helper;

namespace STExS.Controllers.Submission;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CodeOutputSubmissionController: ControllerBase
{
    private readonly ICodeOutputSubmissionService codeOutputSubmissionService;
    
    private readonly ISubmissionService submissionService;

    public CodeOutputSubmissionController(ICodeOutputSubmissionService submissionService, ISubmissionService submissionService1)
    {
        this.codeOutputSubmissionService = submissionService ?? throw new ArgumentNullException(nameof(submissionService));
        this.submissionService = submissionService1 ?? throw new ArgumentNullException(nameof(submissionService1));
    }

    #region User Routes

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Route("submit/{timeTrackId:guid}")]
    public async Task<IActionResult> SubmitCodeOutputSubmission([FromBody] CodeOutputSubmissionCreateItem createItem, Guid timeTrackId, [FromQuery]bool isFinalSubmission, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        try
        {
            await this.codeOutputSubmissionService.SubmitAsync(userId, createItem.ExerciseId, isFinalSubmission, createItem.SubmittedAnswer, timeTrackId, cancellationToken);
        }
        catch (AlreadySubmittedException e)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        return this.Ok();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CodeOutputSubmissionDetailItem))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Route("get/{codeOutputExerciseId:guid}")]
    public async Task<IActionResult> TryGetLastCodeOutputSubmission([FromRoute]Guid codeOutputExerciseId, [FromQuery]Guid currentTimeTrackId, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        try
        {
            var submission = await this.submissionService.GetLastSubmissionForAnsweringAsync(userId, codeOutputExerciseId, currentTimeTrackId, cancellationToken);
            
            // if Submission is instance of CodeOutputSubmission
            if (submission is not CodeOutputSubmission codeOutputSubmission) 
                return this.NotFound();
            
            var submissionDetailItem = new CodeOutputSubmissionDetailItem
            {
                SubmittedAnswer = codeOutputSubmission.SubmittedAnswer,
                SubmittedAt = codeOutputSubmission.CreationTime
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

public class CodeOutputSubmissionDetailItem: CodeOutputSubmissionCreateItem
{
    public DateTime SubmittedAt { get; set; }
}

public class CodeOutputSubmissionCreateItem
{
    public string SubmittedAnswer { get; set; } = string.Empty;
    public Guid ExerciseId { get; set; }
}