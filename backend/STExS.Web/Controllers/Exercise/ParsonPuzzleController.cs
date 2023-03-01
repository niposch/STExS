using Application.DTOs.ExercisesDTOs.Parson;
using Application.Helper.Roles;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STExS.Helper;

namespace STExS.Controllers.Exercise;

[Route("api/[controller]")]
[ApiController]
public class ParsonPuzzleController : ControllerBase
{
    private readonly IAccessService accessService;
    private readonly IParsonExerciseService parsonExerciseService;


    public ParsonPuzzleController(IParsonExerciseService parsonExerciseService, IAccessService accessService)
    {
        this.parsonExerciseService = parsonExerciseService ?? throw new ArgumentNullException(nameof(parsonExerciseService));
        this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
    }

    #region Module Participant  Routes

    [HttpGet]
    [Authorize(Roles = $"{RoleHelper.Admin}, {RoleHelper.User}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ParsonExerciseDetailItem))]
    public async Task<IActionResult> GetExercise([FromQuery] Guid id, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        var res = await this.parsonExerciseService.GetByIdAsync(id, userId, cancellationToken);
        return this.Ok(res);
    }

    #endregion

    #region Administration

    [HttpPost("create")]
    [Authorize(Roles = $"{RoleHelper.Admin},{RoleHelper.Teacher}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ParsonExerciseDetailItemWithAnswer))]
    public async Task<IActionResult> CreateExercise([FromBody] ParsonExerciseCreateItem createItem, CancellationToken cancellationToken = default)
    {
        if (!await this.accessService.IsChapterAdmin(createItem.ChapterId, this.User.GetUserId(), cancellationToken)) return this.Unauthorized();

        var res = await this.parsonExerciseService.CreateAsync(createItem, this.User.GetUserId(), cancellationToken);
        return this.Ok(res);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateExercise([FromBody] ParsonExerciseDetailItemWithAnswer updateItem,
        CancellationToken cancellationToken = default)
    {
        if (!await this.accessService.IsChapterAdmin(updateItem.ChapterId, this.User.GetUserId(), cancellationToken)) return this.Unauthorized();
        /*FIXME UpdateAsync return type void so can't pass to Ok() akin to CodeOutPutController implementation. Fix required?*/
        await this.parsonExerciseService.UpdateAsync(updateItem, cancellationToken);
        return this.Ok("Update Done");
    }

    [HttpGet("withAnswers")]
    [Authorize(Roles = $"{RoleHelper.Admin}, {RoleHelper.Teacher}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ParsonExerciseDetailItemWithAnswer))]
    public async Task<IActionResult> GetExerciseWithAnswers([FromQuery] Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var res = await this.parsonExerciseService.GetByIdWithAnswerAsync(exerciseId, this.User.GetUserId(), cancellationToken);

        if (!await this.accessService.IsChapterAdmin(res.ChapterId, this.User.GetUserId(), cancellationToken)) return this.Unauthorized();
        return this.Ok(res);
    }

    #endregion
}