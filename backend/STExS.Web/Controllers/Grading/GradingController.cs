using Application.DTOs;
using Application.Services.Grading;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using STExS.Helper;

namespace STExS.Controllers.Grading;

[ApiController]
[Route("api/[controller]")]
public class GradingController: ControllerBase
{
    private readonly IGradingService gradingService;

    private readonly ISubmissionService submissionService;

    private readonly IAccessService accessService;

    public GradingController(IGradingService gradingService, IAccessService accessService, ISubmissionService submissionService)
    {
        this.gradingService = gradingService ?? throw new ArgumentNullException(nameof(gradingService));
        this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
        this.submissionService = submissionService ?? throw new ArgumentNullException(nameof(submissionService));
    }

    [HttpGet("chapter")]
    public async Task<IActionResult> GetChapterReport()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("module")]
    public async Task<IActionResult> GetModuleReport()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("exercise")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExerciseReportItem>))]
    public async Task<IActionResult> GetExerciseReport([FromQuery]Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var res = await this.gradingService.GetExerciseReportAsync(exerciseId, cancellationToken);

        return this.Ok(res);
    }
    
    [HttpPost("manualGrading")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GradeExercise(Guid submissionId, int newGrade, string comment, CancellationToken cancellationToken = default)
    {
        await this.gradingService.ManuallyGradeExerciseAsync(submissionId, newGrade, comment, this.User.GetUserId(), cancellationToken);
        return this.Ok();
    }
    
    [HttpGet("submissionsForUserAndExercise")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SubmissionDetailItem>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSubmissionsForUserAndExercise(Guid exerciseId, Guid userId, CancellationToken cancellationToken = default)
    {
        if (!await this.accessService.IsExerciseAdminAsync(exerciseId, this.User.GetUserId(), cancellationToken))
            return this.StatusCode(StatusCodes.Status401Unauthorized);
        
        var res = await this.submissionService.GetSubmissionsForUserAndExerciseAsync(exerciseId, userId, cancellationToken);
        return this.Ok(res);
    }
}