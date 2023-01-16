using Application.DTOs.ExercisesDTOs;
using Application.Helper.Roles;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace STExS.Controllers.Exercise;

[ApiController]
[Route("api/[controller]")]
public class ExerciseController: ControllerBase
{
    private readonly IExerciseService exerciseService;

    public ExerciseController(IExerciseService exerciseService)
    {
        this.exerciseService = exerciseService ?? throw new ArgumentNullException(nameof(exerciseService));
    }
    
    [HttpDelete]
    [Authorize(Roles = $"{RoleHelper.Admin}, {RoleHelper.Teacher}")]
    public async Task<IActionResult> Delete([FromQuery] Guid exerciseId, CancellationToken cancellationToken = default)
    {
        await this.exerciseService.DeleteExerciseAsync(exerciseId, cancellationToken);

        return this.Ok();
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExerciseDetailItem))]
    public async Task<IActionResult> GetById([FromQuery] Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var exercise = await this.exerciseService.GetExerciseByIdAsync(exerciseId, cancellationToken);

        return this.Ok(exercise);
    }
    

    [HttpGet("byChapterId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExerciseDetailItem>))]
    public async Task<IActionResult> GetByChapterId([FromQuery] Guid chapterId, CancellationToken cancellationToken = default)
    {
        var data = await this.exerciseService.GetByChapterIdAsync(chapterId, cancellationToken);
        return this.Ok(data);
    }
    
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExerciseDetailItem>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var data = await this.exerciseService.GetAllAsync(cancellationToken);
        return this.Ok(data);
    }
    
    [HttpPost("copyExercise")]
    [Authorize(Roles = $"{RoleHelper.Admin}, {RoleHelper.Teacher}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExerciseDetailItem))]
    public async Task<IActionResult> CopyExercise([FromQuery] Guid exerciseId, [FromQuery] Guid toChapter, CancellationToken cancellationToken = default)
    {
        var res = await this.exerciseService.CopyToChapterAsync(exerciseId, toChapter, cancellationToken);
        return this.Ok(res);
    }
}