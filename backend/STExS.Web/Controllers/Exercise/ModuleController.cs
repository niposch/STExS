using Application.Helper.Roles;
using Application.Services.Interfaces;
using Common.Models.ExerciseSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STExS.Helper;

namespace STExS.Controllers.Exercise;

[ApiController]
[Route("api/[controller]")]
public class ModuleController: ControllerBase
{
    private readonly IModuleService moduleService;

    public ModuleController(IModuleService moduleService)
    {
        this.moduleService = moduleService ?? throw new ArgumentNullException(nameof(moduleService));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(Roles=$"{RoleHelper.Admin},{RoleHelper.Teacher}")]
    public async Task<IActionResult> CreateModuleAsync(ModuleCreateItem module, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        await this.moduleService.CreateModuleAsync(module.ModuleName, module.ModuleDescription, userId);
        return this.Ok();
    }

    [HttpPut("{moduleId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> UpdateModuleAsync([FromRoute]Guid moduleId, [FromBody]ModuleUpdateItem updateItem, CancellationToken cancellationToken = default)
    {
        var existingModule = await this.moduleService.GetModuleByIdAsync(moduleId, cancellationToken);
        if (existingModule == null)
        {
            return this.NotFound();
        }

        ModuleMapper.UpdateModule(updateItem, existingModule);
        await this.moduleService.UpdateModuleAsync(existingModule, cancellationToken);
        return this.Ok();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> DeleteModuleAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
        await this.moduleService.DeleteModuleAsync(moduleId, cancellationToken);
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
    public async Task<IActionResult> GetModuleByIdAsync([FromQuery]Guid id, CancellationToken cancellationToken = default)
    {
        var res = await this.moduleService.GetModuleByIdAsync(id, cancellationToken);
        return this.Ok(res);
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
    public async Task<IActionResult> GetModulesUserIsAcceptedInto([FromQuery]Guid userId, CancellationToken cancellationToken = default)
    {
        var res = await this.moduleService.GetModulesUserIsAcceptedIntoAsync(userId, cancellationToken);
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
    public async Task<IActionResult> GetUsersInvitedToModule([FromQuery]Guid moduleId, CancellationToken cancellationToken = default)
    {
        var res = await this.moduleService.GetUsersInvitedToModuleAsync(moduleId, cancellationToken);

        return this.Ok(res);
    }
}

public static class ModuleMapper
{
    public static ModuleDetailItem ToDetailItem(Module module, Guid? userId = default)
    {
        return new ModuleDetailItem
        {
            ModuleId = module.Id,
            OwnerFirstName = module.Owner?.FirstName ?? string.Empty,
            OwnerLastName = module.Owner?.LastName ?? string.Empty,
            OwnerId = module.OwnerId,
            ModuleName = module.ModuleName,
            ModuleDescription = module.ModuleDescription,
            ArchivedDate = module.ArchivedDate,
            DeletedDate = module.DeletedDate,
            ChapterIds = module.Chapters?.Select(c => c.Id).ToList() ?? new List<Guid>(),
            IsFavorited =  module.OwnerId == userId || (userId != null && (module.ModuleParticipations?.Any(r => r.UserId == userId) ?? false))
        };
    }
    
    public static Module ToModule(ModuleCreateItem moduleCreateItem, Guid changeUserId)
    {
        return new Module
        {
            Id = Guid.NewGuid(),
            ModuleName = moduleCreateItem.ModuleName,
            ModuleDescription = moduleCreateItem.ModuleDescription,
            OwnerId = changeUserId,
            ArchivedDate = null,
            DeletedDate = null,
            Chapters = new List<Chapter>()
        };
    }
    
    public static Module UpdateModule(ModuleUpdateItem moduleUpdateItem, Module module)
    {
        module.ModuleName = moduleUpdateItem.ModuleName;
        module.ModuleDescription = moduleUpdateItem.ModuleDescription;
        return module;
    }
}

public sealed class ModuleDetailItem {
    public Guid ModuleId { get; set; }
    
    public Guid OwnerId { get; set; }
    public string OwnerFirstName { get; set; } = string.Empty;
    public string OwnerLastName { get; set; } = string.Empty;
    
    public string ModuleName { get; set; } = string.Empty;
    public string ModuleDescription { get; set; } = string.Empty;
    public DateTime? ArchivedDate { get; set; }
    public DateTime? DeletedDate { get; set; }

    public bool IsArchived => this.ArchivedDate.HasValue;
    public bool IsDeleted => this.DeletedDate.HasValue;

    public List<Guid> ChapterIds { get; set; } = new List<Guid>();
    
    public bool IsFavorited { get; set; }
}

public sealed class ModuleCreateItem {
    public string ModuleName { get; set; } = string.Empty;
    public string ModuleDescription { get; set; } = string.Empty;
}

public sealed class ModuleUpdateItem {
    public string ModuleName { get; set; } = string.Empty;
    public string ModuleDescription { get; set; } = string.Empty;
}