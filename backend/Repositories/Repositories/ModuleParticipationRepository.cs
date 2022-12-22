using Common.Exceptions;
using Common.Models.Authentication;
using Common.Models.ExerciseSystem;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.GenericImplementations;

namespace Repositories.Repositories;

public class ModuleParticipationRepository: IModuleParticipationRepository
{
    private readonly ApplicationDbContext context;
    public ModuleParticipationRepository(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task RegisterUserInModuleAsync(Guid userId, Guid moduleId, CancellationToken cancellationToken = default)
    {
        var existingItem = await this.context.ModuleParticipations
            .Include(c => c.User)
            .Include(c => c.Module)
            .FirstOrDefaultAsync(p =>
                p.UserId == userId &&
                p.ModuleId == moduleId, cancellationToken);
        if(existingItem != null)
        {
            throw new UserModuleRegistrationException(existingItem.User.UserName,
                existingItem.Module.ModuleName,
                existingItem.ParticipationConfirmed);
        }
        var newParticipation = new ModuleParticipation
        {
            CreationTime = DateTime.Now,
            ModuleId = moduleId,
            UserId = userId,
            ParticipationConfirmed = false
        };
        this.context.ModuleParticipations.Add(newParticipation);

        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UnregisterUserFromModuleAsync(Guid userId, Guid moduleId, CancellationToken cancellationToken = default)
    {
        var existingItem = await this.context.ModuleParticipations
            .Include(c => c.User)
            .Include(c => c.Module)
            .FirstOrDefaultAsync(p =>
                p.UserId == userId &&
                p.ModuleId == moduleId, cancellationToken);
        if(existingItem == null)
        {
            var userName =  await this.context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.UserName)
                .FirstOrDefaultAsync(cancellationToken);
            var moduleName = await this.context.Modules
                .Where(m => m.Id == moduleId)
                .Select(m => m.ModuleName)
                .FirstOrDefaultAsync(cancellationToken);
            throw new UserModuleUnregistrationException(userName??string.Empty, moduleName??string.Empty);
        }
        this.context.ModuleParticipations.Remove(existingItem);

        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsUserRegisteredInModuleAsync(Guid userId, Guid moduleId, CancellationToken cancellationToken = default)
    {
        return await this.context.ModuleParticipations
            .AnyAsync(p =>
                p.UserId == userId &&
                p.ModuleId == moduleId, cancellationToken);
    }

    public async Task<IEnumerable<ModuleParticipation>> GetParticipationsForModuleAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
        return await this.context.ModuleParticipations
            .Where(p => p.ModuleId == moduleId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ModuleParticipation>> GetParticipationsForUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await this.context.ModuleParticipations
            .Where(p => p.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task TryConfirmParticipationAsync(Guid userId, Guid moduleId, CancellationToken cancellationToken = default)
    {
        var existingItem = await this.context.ModuleParticipations
            .FirstOrDefaultAsync(p =>
                p.UserId == userId &&
                p.ModuleId == moduleId, cancellationToken);
        if(existingItem == null)
        {
            var userName =  await this.context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.UserName)
                .FirstOrDefaultAsync(cancellationToken);
            var moduleName = await this.context.Modules
                .Where(m => m.Id == moduleId)
                .Select(m => m.ModuleName)
                .FirstOrDefaultAsync(cancellationToken);
            throw new UserModuleUnregistrationException(userName??string.Empty, moduleName??string.Empty);
        }
        existingItem.ParticipationConfirmed = true;
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task TryUnconfirmParticipationAsync(Guid userId, Guid moduleId, CancellationToken cancellationToken = default)
    {
        var existingItem = await this.context.ModuleParticipations
            .FirstOrDefaultAsync(p =>
                p.UserId == userId &&
                p.ModuleId == moduleId, cancellationToken);
        if(existingItem == null)
        {
            var userName =  await this.context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.UserName)
                .FirstOrDefaultAsync(cancellationToken);
            var moduleName = await this.context.Modules
                .Where(m => m.Id == moduleId)
                .Select(m => m.ModuleName)
                .FirstOrDefaultAsync(cancellationToken);
            throw new UserModuleUnregistrationException(userName??string.Empty, moduleName??string.Empty);
        }
        existingItem.ParticipationConfirmed = false;
        await this.context.SaveChangesAsync(cancellationToken);
    }
}