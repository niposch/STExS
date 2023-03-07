using Application.Helper.Roles;
using Application.Services.Grading;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STExS.Helper;

namespace STExS.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class TimeTrackController: ControllerBase
{
    private readonly ITimeTrackService timeTrackService;
    
    private readonly IAccessService accessService;
    
    public TimeTrackController(ITimeTrackService timeTrackService, IAccessService accessService)
    {
        this.timeTrackService = timeTrackService ?? throw new ArgumentNullException(nameof(timeTrackService));
        this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
    }
    
    [HttpGet("{timeTrackId}")]
    [ProducesResponseType(typeof(TimeTrackDetailItem), 200)]
    [Authorize(Roles = $"{RoleHelper.Teacher},{RoleHelper.Admin}")]
    public async Task<IActionResult> GetTimeTrackAsync(Guid timeTrackId, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        var res = await timeTrackService.GetTimeTrackAsync(userId, timeTrackId, cancellationToken);
        
        return this.Ok(res);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Guid), 200)]
    public async Task<IActionResult> CreateTimeTrackAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        var res = await timeTrackService.CreateTimeTrackAsync(userId, exerciseId, cancellationToken);
        
        return this.Ok(res);
    }
    
    [HttpPost("close")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> CloseTimeTrackAsync(Guid timeTrackId, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        await timeTrackService.CloseTimeTrackAsync(timeTrackId, cancellationToken);
        
        return this.Ok();
    }
    
    [HttpGet("timeTracksForExerciseAndUser")]
    [ProducesResponseType(typeof(List<TimeTrackEvent>), 200)]
    [Authorize(Roles = $"{RoleHelper.Teacher},{RoleHelper.Admin}")]
    public async Task<IActionResult> GetTimeTracksForExerciseAndUserAsync(Guid exerciseId, Guid userId, CancellationToken cancellationToken = default)
    {
        if (!await this.accessService.IsExerciseAdminAsync(exerciseId, this.User.GetUserId(), cancellationToken))
        {
            return this.StatusCode(StatusCodes.Status401Unauthorized);
        }
        var res = await timeTrackService.GetTimeTracksForExerciseAndUserAsync(exerciseId, userId, cancellationToken);
        
        return this.Ok(res);
    }
}