namespace Application.DTOs.ModuleDTOs;

public sealed class ModuleParticipationDetailItem
{
    public Guid ModuleId { get; set; }
    
    public Guid UserId { get; set; }
    
    public bool ParticipationConfirmed { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public bool IsFavorite { get; set; }
}