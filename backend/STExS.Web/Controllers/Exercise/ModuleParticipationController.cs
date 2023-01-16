using Application.DTOs.ModuleDTOs;
using Application.Helper.Roles;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STExS.Helper;

namespace STExS.Controllers.Exercise;

[ApiController]
[Route("api/[controller]")]
public class ModuleParticipationController: ControllerBase
{
    private IModuleService moduleService;

    public ModuleParticipationController(IModuleService moduleService)
    {
        this.moduleService = moduleService ?? throw new ArgumentNullException(nameof(moduleService));
    }

    [HttpGet("forModule")]
    [ProducesResponseType(typeof(IEnumerable<ModuleParticipationDetailItem>), 200)]
    public async Task<IActionResult> GetAllParticipationsForModule([FromQuery]Guid moduleId, CancellationToken cancellationToken = default)
    {
        var res = await this.moduleService.GetParticipationsForModuleAsync(moduleId, cancellationToken);
        
        return this.Ok(res);
    }
    
    
    [HttpGet("forAdminUser")]
    [ProducesResponseType(typeof(IEnumerable<ModuleParticipationDetailItem>), 200)]
    [Authorize(Roles = $"{RoleHelper.Admin},{RoleHelper.Teacher}")]
    public async Task<IActionResult> GetAllParticipationsForAdminUser(CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        var res = await this.moduleService.GetParticipationsForAdminUserAsync(userId, cancellationToken);
        
        return this.Ok(res);
    }
    
    
    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmParticipation([FromQuery] Guid moduleId, [FromQuery] Guid userId, CancellationToken cancellationToken = default)
    {
        await this.moduleService.ConfirmModuleParticipationAsync(moduleId, userId, cancellationToken);
        
        return this.Ok();
    }
    
    [HttpPost("reject")]
    public async Task<IActionResult> RejectParticipation([FromQuery] Guid moduleId, [FromQuery] Guid userId, CancellationToken cancellationToken = default)
    {
        await this.moduleService.RejectModuleParticipationAsync(moduleId, userId, cancellationToken);
        
        return this.Ok();
    }
}
