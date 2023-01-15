using Application.DTOs.ModuleDTOs;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    
    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmParticipation([FromQuery] Guid moduleId, [FromQuery] Guid userId, CancellationToken cancellationToken = default)
    {
        await this.moduleService.ConfirmModuleParticipationAsync(moduleId, userId, cancellationToken);
        
        return this.Ok();
    }
}
