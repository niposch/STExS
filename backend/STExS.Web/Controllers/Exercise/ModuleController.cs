using Application.DTOs.Module;
using Application.Helper.Roles;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STExS.Helper;

namespace STExS.Controllers.Exercise;

[ApiController]
[Route("api/[controller]")]
public class ModuleController : ControllerBase
{
    private readonly IModuleService moduleService;

    public ModuleController(IModuleService moduleService)
    {
        this.moduleService = moduleService ?? throw new ArgumentNullException(nameof(moduleService));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(Roles = $"{RoleHelper.Admin},{RoleHelper.Teacher}")]
    public async Task<IActionResult> CreateModuleAsync(ModuleCreateItem module, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        await this.moduleService.CreateModuleAsync(module.ModuleName, module.ModuleDescription, module.MaxParticipants, userId, cancellationToken);
        return this.Ok();
    }

    [HttpPut("{moduleId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> UpdateModuleAsync([FromRoute] Guid moduleId, [FromBody] ModuleUpdateItem updateItem, CancellationToken cancellationToken = default)
    {
        await this.moduleService.UpdateModuleAsync(moduleId, updateItem.ModuleName, updateItem.ModuleDescription, updateItem.MaxParticipants, cancellationToken);
        return this.Ok();
    }

    [HttpDelete("{moduleId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(Roles=$"{RoleHelper.Admin},{RoleHelper.Teacher}")]
    public async Task<IActionResult> DeleteModuleAsync([FromRoute]Guid moduleId, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        await this.moduleService.DeleteModuleAsync(moduleId, userId, cancellationToken);
        return this.Ok();
    }

    [HttpPost("archive")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> ArchiveModuleAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
        await this.moduleService.ArchiveModuleAsync(moduleId, cancellationToken);
        return this.Ok();
    }

    [HttpPost("unarchive")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> UnarchiveModuleAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
        await this.moduleService.UnarchiveModuleAsync(moduleId, cancellationToken);

        return this.Ok();
    }

    [HttpGet("getById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModuleDetailItem))]
    [Authorize]
    public async Task<IActionResult> GetModuleByIdAsync([FromQuery] Guid id, CancellationToken cancellationToken = default)
    {
        var res = await this.moduleService.GetModuleByIdAsync(id, cancellationToken);
        return this.Ok(ModuleMapper.ToDetailItem(res));
    }

    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ModuleDetailItem>))]
    [Authorize]
    public async Task<IActionResult> GetModulesAsync(CancellationToken cancellationToken = default)
    {
        var res = await this.moduleService.GetModulesAsync(cancellationToken);
        return this.Ok(res);
    }

    [HttpGet("getModulesForUser")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ModuleDetailItem>))]
    [Authorize]
    public async Task<IActionResult> GetModulesUserIsAcceptedInto(CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        var res = await this.moduleService.GetModulesUserIsAcceptedIntoAsync(userId, cancellationToken);
        return this.Ok(res.Select(m => ModuleMapper.ToDetailItem(m, userId)));
    }

    [HttpGet("getModulesForUserAdmin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ModuleDetailItem>))]
    [Authorize(Roles = $"{RoleHelper.Admin}")]
    public async Task<IActionResult> GetModulesUserIsAcceptedInto(Guid userId, CancellationToken cancellationToken = default)
    {
        var res = await this.moduleService.GetModulesUserIsAcceptedIntoAsync(userId, cancellationToken);
        return this.Ok(res.Select(m => ModuleMapper.ToDetailItem(m, userId)));
    }

    [HttpGet("getModulesUserIsAdminOf")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ModuleDetailItem>))]
    [Authorize(Roles = $"{RoleHelper.Admin},{RoleHelper.Teacher}")]
    public async Task<IActionResult> GetModulesUserIsAdminOf([FromQuery] Guid? userId = null, CancellationToken cancellationToken = default)
    {
        var res = await this.moduleService.GetModulesUserIsAdminOfAsync(userId ?? this.User.GetUserId(), cancellationToken);
        return this.Ok(res.Select(m => ModuleMapper.ToDetailItem(m, userId)));
    }

    [HttpGet("getArchived")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ModuleDetailItem>))]
    [Authorize]
    public async Task<IActionResult> GetArchivedModulesAsync(CancellationToken cancellationToken = default)
    {
        var res = await this.moduleService.GetArchivedModulesAsync(cancellationToken);

        return this.Ok(res);
    }

    [HttpGet("getUsersForModule")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ModuleDetailItem>))]
    [Authorize]
    public async Task<IActionResult> GetUsersInvitedToModule([FromQuery] Guid moduleId, CancellationToken cancellationToken = default)
    {
        var res = await this.moduleService.GetParticipationsForUserAsync(moduleId, cancellationToken);

        return this.Ok(res);
    }

    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ModuleDetailItem>))]
    [Authorize]
    public async Task<IActionResult> SearchModulesAsync([FromQuery] string? search = null, CancellationToken cancellationToken = default)
    {
        var res = await this.moduleService.SearchModulesAsync(search ?? string.Empty, cancellationToken);

        return this.Ok(res
            .Take(100)
            .Select(m => ModuleMapper.ToDetailItem(m))
            .ToList());
    }

    [HttpPost("joinModule")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> JoinModuleAsync([FromQuery] Guid moduleId, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        await this.moduleService.JoinModuleAsync(moduleId, userId, cancellationToken);
        return this.Ok();
    }
    
    [HttpGet("getModuleParticipationStatus")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModuleParticipationStatus))]
    [Authorize]
    public async Task<IActionResult> GetModuleParticipationStatusAsync([FromQuery] Guid moduleId, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        var res = await this.moduleService.GetModuleParticipationStatusAsync(moduleId, userId, cancellationToken);
        return this.Ok(res);
    }
    
    [HttpGet("getModuleParticipantCount")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [Authorize]
    public async Task<IActionResult> GetModuleParticipantCountAsync([FromQuery] Guid moduleId, CancellationToken cancellationToken = default)
    {
        var res = await this.moduleService.GetModuleParticipantCountAsync(moduleId, cancellationToken);
        return this.Ok(res);
    }
}