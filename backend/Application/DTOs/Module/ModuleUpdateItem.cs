namespace STExS.Controllers.Exercise;

public sealed class ModuleUpdateItem
{
    public string ModuleName { get; set; } = string.Empty;
    public string ModuleDescription { get; set; } = string.Empty;

    public int? MaxParticipants { get; set; } = null;
}