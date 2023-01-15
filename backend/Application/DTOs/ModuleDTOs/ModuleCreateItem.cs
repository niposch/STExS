namespace Application.DTOs.ModuleDTOs;

public sealed class ModuleCreateItem
{
    public string ModuleName { get; set; } = string.Empty;
    public string ModuleDescription { get; set; } = string.Empty;

    public int MaxParticipants { get; set; } = 0;
}