using System.Globalization;
using Common.Models.ExerciseSystem;

namespace Application.DTOs.ModuleDTOs;

public sealed class ModuleDetailItem
{
    public Guid ModuleId { get; set; }

    public Guid OwnerId { get; set; }
    public string OwnerFirstName { get; set; } = string.Empty;
    public string OwnerLastName { get; set; } = string.Empty;

    public string ModuleName { get; set; } = string.Empty;
    public string ModuleDescription { get; set; } = string.Empty;
    public DateTime? ArchivedDate { get; set; }
    public bool IsArchived => this.ArchivedDate.HasValue;

    public List<Guid> ChapterIds { get; set; } = new();

    public bool IsFavorited { get; set; }

    public string teacherName { get; set; }
    
    public DateTime CreationTime { get; set; }
    
    public int? MaxParticipants { get; set; }
    
    public ModuleParticipationStatus? CurrentUserParticipationStatus { get; set; }

    public static ModuleDetailItem ToDetailItem(Module module, Guid? userId = null)
    {
        var currentUserParticipation = userId != null ? module.ModuleParticipations.FirstOrDefault(p => p.UserId == userId) : null;
        return new ModuleDetailItem
        {
            ModuleId = module.Id,
            OwnerId = module.OwnerId,
            OwnerFirstName = module.Owner.FirstName,
            OwnerLastName = module.Owner.LastName,
            ModuleName = module.ModuleName,
            ModuleDescription = module.ModuleDescription,
            ArchivedDate = module.ArchivedDate,
            ChapterIds = module.Chapters?.Select(c => c.Id).ToList() ?? new List<Guid>(),
            IsFavorited = currentUserParticipation?.IsFavorite ?? false,
            teacherName = module.Owner.FirstName + " " + module.Owner.LastName,
            CreationTime = module.CreationTime,
            MaxParticipants = module.MaxParticipants,
            CurrentUserParticipationStatus = GetParticipationStatus(module, currentUserParticipation, userId)
        };
    }

    private static ModuleParticipationStatus? GetParticipationStatus(Module  module, ModuleParticipation? participation, Guid? userId)
    {
        if(userId == null) return null;

        if(module.OwnerId == userId){
            return ModuleParticipationStatus.Admin;
        }

        if (participation?.ParticipationConfirmed ?? false)
        {
            return ModuleParticipationStatus.Accepted;
        }

        return ModuleParticipationStatus.NotParticipating;
    }
}