using Application.DTOs;
using Application.DTOs.ExercisesDTOs;
using Application.DTOs.GradingReportDTOs;
using Application.DTOs.ModuleDTOs;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using STExS.Controllers.Exercise;

namespace STExS.Controllers.Grading;

[ApiController]
[Route("api/[controller]")]
public class GradingController : ControllerBase
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModuleReport))]
    public async Task<IActionResult> GetModuleReport([FromQuery] Guid moduleId, CancellationToken cancellationToken = default)
    {
        return this.Ok(await this.gradingService.GetModuleReportAsync(moduleId, cancellationToken));
    }

    [HttpGet("exercise")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExerciseReportItem>))]
    public async Task<IActionResult> GetExerciseReport([FromQuery] Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var res = await this.gradingService.GetExerciseReportAsync(exerciseId, cancellationToken);

        return this.Ok(res);
    }
}