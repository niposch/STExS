﻿using Application.DTOs.ExercisesDTOs.CodeOutput;
using Application.Helper.Roles;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STExS.Helper;

namespace STExS.Controllers.Exercise;

[ApiController]
[Route("api/[controller]")]
public class CodeOutputController : ControllerBase
{
    private readonly IAccessService accessService;
    private readonly ICodeOutputExerciseService codeOutputExerciseService;

    public CodeOutputController(ICodeOutputExerciseService codeOuputExerciseService, IAccessService accessService)
    {
        this.codeOutputExerciseService = codeOuputExerciseService ?? throw new ArgumentNullException(nameof(codeOuputExerciseService));
        this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
    }

    #region Module Participant  Routes

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CodeOutputDetailItem))]
    public async Task<IActionResult> GetExercise([FromQuery] Guid id, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        var res = await this.codeOutputExerciseService.GetByIdAsync(id, userId, cancellationToken);
        return this.Ok(res);
    }

    #endregion

    #region Module Admin Routes

    [HttpPost("create")]
    [Authorize(Roles = $"{RoleHelper.Admin},{RoleHelper.Teacher}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CodeOutputExerciseDetailItemWithAnswer))]
    public async Task<IActionResult> CreateExercise([FromBody] CodeOutputExerciseCreateItem createItem, CancellationToken cancellationToken = default)
    {
        if (!await this.accessService.IsChapterAdmin(createItem.ChapterId, this.User.GetUserId())) return this.Unauthorized();

        var res = await this.codeOutputExerciseService.CreateAsync(createItem, this.User.GetUserId(), cancellationToken);
        return this.Ok(res);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateExercise([FromBody] CodeOutputExerciseDetailItemWithAnswer createItem,
        CancellationToken cancellationToken = default)
    {
        if (!await this.accessService.IsChapterAdmin(createItem.ChapterId, this.User.GetUserId(), cancellationToken)) return this.Unauthorized();

        var res = await this.codeOutputExerciseService.UpdateAsync(createItem, cancellationToken);
        return this.Ok(res);
    }

    [HttpGet("withAnswers")]
    [Authorize(Roles = $"{RoleHelper.Admin}, {RoleHelper.Teacher}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CodeOutputExerciseDetailItemWithAnswer))]
    public async Task<IActionResult> GetExerciseWithAnswers([FromQuery] Guid id, CancellationToken cancellationToken = default)
    {
        var res = await this.codeOutputExerciseService.GetByIdWithAnswerAsync(id, this.User.GetUserId(), cancellationToken);

        if (!await this.accessService.IsChapterAdmin(res.ChapterId, this.User.GetUserId(), cancellationToken)) return this.Unauthorized();
        return this.Ok(res);
    }

    #endregion
}