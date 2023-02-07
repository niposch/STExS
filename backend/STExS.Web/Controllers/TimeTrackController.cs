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
    
    public TimeTrackController(ITimeTrackService timeTrackService)
    {
        this.timeTrackService = timeTrackService ?? throw new ArgumentNullException(nameof(timeTrackService));
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
    
    /*
     implement these methods:
    public Task<Guid> CreateTimeTrackAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
    public Task ReportActivityAsync(Guid timeTrackId, CancellationToken cancellationToken = default);
    public Task<Guid> CloseTimeTrackAsync(Guid timeTrackId, CancellationToken cancellationToken = default);
    public Task<TimeTrackDetailItem> GetTimeTrackAsync(Guid timeTrackId, CancellationToken cancellationToken = default);
*/
    
    [HttpPost]
    [ProducesResponseType(typeof(Guid), 200)]
    public async Task<IActionResult> CreateTimeTrackAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        var res = await timeTrackService.CreateTimeTrackAsync(userId, exerciseId, cancellationToken);
        
        return this.Ok(res);
    }
}