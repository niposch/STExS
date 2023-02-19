using Application.DTOs;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace STExS.Controllers.Grading;

[ApiController]
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
    public async Task<IActionResult> GetExerciseReport(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var res = await this.gradingService.GetExerciseReportAsync(exerciseId, cancellationToken);

        return this.Ok(res);
    }
}