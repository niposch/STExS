using Common.Models.ExerciseSystem;

namespace Application.DTOs.Module;

public static class ModuleMapper
{
    public static ModuleDetailItem ToDetailItem(Common.Models.ExerciseSystem.Module module, Guid? userId = default)
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
            ChapterIds = module.Chapters?.Select(c => c.Id).ToList() ?? new List<Guid>(),
            IsFavorited = module.OwnerId == userId || (userId != null && (module.ModuleParticipations?.Any(r => r.UserId == userId) ?? false)),
            teacherName = module.Owner?.FirstName + " " + module.Owner?.LastName,
            CreationTime = module.CreationTime, 
            MaxParticipants = module.MaxParticipants
        };
    }

    public static Common.Models.ExerciseSystem.Module ToModule(ModuleCreateItem moduleCreateItem, Guid changeUserId)
    {
        return new Common.Models.ExerciseSystem.Module
        {
            Id = Guid.NewGuid(),
            ModuleName = moduleCreateItem.ModuleName,
            ModuleDescription = moduleCreateItem.ModuleDescription,
            OwnerId = changeUserId,
            ArchivedDate = null,
            Chapters = new List<Chapter>(),
        };
    }

    public static Common.Models.ExerciseSystem.Module UpdateModule(ModuleUpdateItem moduleUpdateItem, Common.Models.ExerciseSystem.Module module)
    {
        module.ModuleName = moduleUpdateItem.ModuleName;
        module.ModuleDescription = moduleUpdateItem.ModuleDescription;
        return module;
    }
}