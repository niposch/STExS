using Application.DTOs;
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

    public GradingController(IGradingService gradingService)
    {
        this.gradingService = gradingService;
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
    
}