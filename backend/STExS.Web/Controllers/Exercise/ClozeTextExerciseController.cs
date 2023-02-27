using Application.DTOs.ExercisesDTOs.ClozeText;
using Application.Helper.Roles;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STExS.Helper;

namespace STExS.Controllers.Exercise;

[Route("api/[controller]")]
[ApiController]
public sealed class ClozeTextExerciseController : ControllerBase
{
    private readonly IAccessService accessService;
    private readonly IClozeTextExerciseService clozeTextExerciseService;

    public ClozeTextExerciseController(IClozeTextExerciseService codeOuputExerciseService, IAccessService accessService)
    {
        this.clozeTextExerciseService = codeOuputExerciseService ?? throw new ArgumentNullException(nameof(codeOuputExerciseService));
        this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
    }


    [HttpGet("withoutAnswers")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClozeTextExerciseDetailItem))]
    public async Task<IActionResult> GetExercise([FromQuery] Guid id, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        var res = await this.clozeTextExerciseService.GetByIdAsync(id, userId, false, cancellationToken);
        return this.Ok(res);
    }

    #region Module Admin Routes

    [HttpPost("create")]
    [Authorize(Roles = $"{RoleHelper.Admin},{RoleHelper.Teacher}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClozeTextExerciseDetailItem))]
    public async Task<IActionResult> CreateExercise([FromBody] ClozeTextExerciseCreateItem createItem, CancellationToken cancellationToken = default)
    {
        if (!await this.accessService.IsChapterAdmin(createItem.ChapterId, this.User.GetUserId(), cancellationToken)) return this.Unauthorized();

        var res = await this.clozeTextExerciseService.CreateAsync(createItem, this.User.GetUserId(), cancellationToken);
        return this.Ok(res);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateExercise([FromBody] ClozeTextExerciseDetailItem createItem,
        CancellationToken cancellationToken = default)
    {
        if (!await this.accessService.IsChapterAdmin(createItem.ChapterId, this.User.GetUserId(), cancellationToken)) return this.Unauthorized();

        await this.clozeTextExerciseService.UpdateAsync(createItem, cancellationToken);
        return this.Ok();
    }

    [HttpGet("withAnswers")]
    [Authorize(Roles = $"{RoleHelper.Admin}, {RoleHelper.Teacher}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClozeTextExerciseDetailItem))]
    public async Task<IActionResult> GetExerciseWithAnswers([FromQuery] Guid id, CancellationToken cancellationToken = default)
    {
        var res = await this.clozeTextExerciseService.GetByIdAsync(id, this.User.GetUserId(), true, cancellationToken);

        if (!await this.accessService.IsChapterAdmin(res.ChapterId, this.User.GetUserId(), cancellationToken)) return this.Unauthorized();
        return this.Ok(res);
    }

    #endregion
}