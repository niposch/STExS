using Application.Helper.Roles;
using Application.Services.Interfaces;
using Common.Models.Authentication;
using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Generic;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Permissions;

public class AccessService : IAccessService
{
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IApplicationRepository repository;

    public AccessService(RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        IApplicationRepository repository)
    {
        this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> CanViewModule(Guid moduleId, Guid userId, CancellationToken cancellationToken = default)
    {
        if (await IsModuleAdmin(moduleId, userId, cancellationToken))
        {
            return true;
        }

        var moduleParticipation = await this.repository.ModuleParticipations.TryGetByModuleAndUserIdAsync(moduleId, userId, cancellationToken);
        return moduleParticipation != null && moduleParticipation.ParticipationConfirmed;
    }
    
    public async Task<IEnumerable<Chapter>> FilterChapterVisibility(IEnumerable<Chapter> chapters, Guid userId, CancellationToken cancellationToken = default)
    {
        if (await IsSystemAdmin(userId))
        {
            return chapters;
        }
        
        
        var moduleIds = chapters.Select(m => m.ModuleId).Distinct();
        var moduleParticipations = await this.repository.ModuleParticipations.GetByModuleIdsAndUserIdAsync(moduleIds, userId, cancellationToken);

        var ownerOfModules = moduleParticipations
            .Select(part => part.Module)
            .Where(m => m.OwnerId == userId)
            .Select(m => m.Id)
            .Distinct()
            .ToList();
        
        var participationsConfirmed = moduleParticipations
            .Where(part => part.ParticipationConfirmed)
            .Select(part => part.Module)
            .Select(m => m.Id)
            .Distinct()
            .ToList();
        
        return chapters.Where(c => ownerOfModules.Contains(c.ModuleId) ||
                                   participationsConfirmed.Contains(c.ModuleId));
    }

    public async Task<bool> IsModuleAdmin(Guid moduleId, Guid userId, CancellationToken cancellationToken = default)
    {
        if (await this.IsSystemAdmin(userId))
        {
            return true;
        }
        
        var module = await this.repository.Modules.TryGetByIdAsync(moduleId, cancellationToken);
        if (module == null)
        {
            return false;
        }
        
        return module.OwnerId == userId;
    }

    public async Task<bool> IsChapterAdmin(Guid chapterId, Guid userId, CancellationToken cancellationToken = default)
    {
        var chapter = await this.repository.Chapters.TryGetByIdAsync(chapterId);
        if (chapter == null)
        {
            return false;
        }

        return await this.IsModuleAdmin(chapter.ModuleId, userId, cancellationToken);
    }

    public async Task<bool> IsSystemAdmin(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        return await userManager.IsInRoleAsync(user, RoleHelper.Admin);
    }
}