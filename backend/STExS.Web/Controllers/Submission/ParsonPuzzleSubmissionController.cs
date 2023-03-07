using Microsoft.AspNetCore.Mvc;
using STExS.Controllers.Submission;
using Microsoft.AspNetCore.Authorization;
using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.ExerciseSystem.Parson;
using STExS.Helper;
using Application.DTOs.ExercisesDTOs.Parson;
using Application.DTOs.ExercisesDTOs;

namespace STExS;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ParsonPuzzleSubmissionController: ControllerBase,
ISubmissionController<ParsonPuzzleSubmissionCreateItem, ParsonPuzzleSubmissionDetailItem>
{

    public ParsonPuzzleSubmissionController(
        ISubmissionService submissionService,
        IAccessService accessService,
        IParsonPuzzleSubmissionService codeOutputSubmissionService)
    {
        this.accessService = accessService;
        this.parsonPuzzleSubmissionService = codeOutputSubmissionService;
        this.submissionService = submissionService;
    }
    private readonly IParsonPuzzleSubmissionService parsonPuzzleSubmissionService;
    
    private readonly ISubmissionService submissionService;

    private readonly IAccessService accessService;

    #region User Routes

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Route("submit/{timeTrackId:guid}")]
    public async Task<IActionResult> SubmitSubmission([FromBody] ParsonPuzzleSubmissionCreateItem createItem, Guid timeTrackId, [FromQuery]bool isFinalSubmission, CancellationToken cancellationToken = default)
    {
        await this.parsonPuzzleSubmissionService.SubmitAsync(
            this.User.GetUserId(),
            createItem.ExerciseId,
            isFinalSubmission,
            createItem.SubmittedLines
                .Select(i => i.Id)
                .ToList(),
            timeTrackId, 
            cancellationToken);
        return this.Ok("Submission Done");
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ParsonPuzzleSubmissionDetailItem))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Route("get/{parsonPuzzleExerciseId:guid}")]
    public async Task<IActionResult> TryGetLastSubmission([FromRoute]Guid parsonPuzzleExerciseId, [FromQuery]Guid? currentTimeTrackId = null, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        try
        {
            var submission = await this.submissionService.GetLastSubmissionForAnsweringAsync(userId, parsonPuzzleExerciseId, currentTimeTrackId, cancellationToken);
            
            // if Submission is instance of ParsonPuzzleSubmission
            if (submission is not ParsonPuzzleSubmission parsonPuzzleSubmission) 
                return this.NotFound();
            
            var submissionDetailItem = new ParsonPuzzleSubmissionDetailItem
            {
                SubmittedLines = parsonPuzzleSubmission.ParsonElements
                    .Select(line => new ParsonExerciseLineDetailItem
                    {
                        Id = line.Id,
                        Indentation = line.Indentation,
                        Text = line.Code,
                    }).ToList(),
                SubmittedAt = parsonPuzzleSubmission.CreationTime,
                ExerciseId = parsonPuzzleExerciseId
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

public class ParsonPuzzleSubmissionDetailItem: ParsonPuzzleSubmissionCreateItem
{
    public DateTime SubmittedAt { get; set; }
}

public class ParsonPuzzleSubmissionCreateItem
{
    public List<ParsonExerciseLineDetailItem> SubmittedLines { get; set; } = new();
    public Guid ExerciseId { get; set; }
}
