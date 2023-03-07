namespace Application.DTOs.ChapterDTOs;
public class ChapterCreateItem
{
    public Guid ModuleId { get; set; }
    public string ChapterName { get; set; } = string.Empty;
    public string ChapterDescription { get; set; } = string.Empty;
}