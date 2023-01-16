namespace Application.DTOs.ModuleDTOs;

public sealed class ModuleParticipationDetailItem
{
    public Guid ModuleId { get; set; }
    
    public Guid UserId { get; set; }
    
    public bool ParticipationConfirmed { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public bool IsFavorite { get; set; }
    
    public string? UserName { get; set; }
    public string? UserFirstName { get; set; }
    public string? UserLastName { get; set; }
    public string? UserEmail { get; set; }
    public string? MatrikelNumber { get; set; }
    
    public string? ModuleName { get; set; }
}