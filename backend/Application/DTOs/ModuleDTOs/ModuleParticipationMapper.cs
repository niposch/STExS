using System.Xml;
using Common.Models.ExerciseSystem;

namespace Application.DTOs.ModuleDTOs;

public static class ModuleParticipationMapper
{
    public static ModuleParticipationDetailItem ToDetailItem(ModuleParticipation entity)
    {
        return new ModuleParticipationDetailItem
        {
            CreationDate = entity.CreationTime,
            IsFavorite = entity.IsFavorite,
            ModuleId = entity.ModuleId,
            ParticipationConfirmed = entity.ParticipationConfirmed,
            UserId = entity.UserId
        };
    }
}