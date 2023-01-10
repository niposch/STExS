using Application.Helper.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace STExS.Controllers.Exercise;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CodeOutputController:ControllerBase
{
    [HttpPost]
    [Authorize(Roles = $"{RoleHelper.Admin}, {RoleHelper.Teacher}")]
    public async Task<IActionResult> CreateExercise()
    {
    }
    
    [HttpGet]
    public Task<IActionResult> GetLatestSubmissionById([FromQuery] Guid codeOutputExerciseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    [HttpPost]
    public async Task<IActionResult> UpdateAnswer([FromQuery] Guid submissionStartId,
        [FromQuery] Guid codeOutputExerciseId,
        [FromBody] CodeOutputSubmissionCreateItem createItem,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

public class CodeOutputSubmissionCreateItem
{
    public string PredictedOutput { get; set; } // what the user thinks the output of the code will be
}