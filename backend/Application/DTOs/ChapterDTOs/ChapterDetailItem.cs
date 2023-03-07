namespace Application.DTOs.ChapterDTOs;
using Application.DTOs;
using Application.DTOs.ExercisesDTOs;
using Common.Models.ExerciseSystem;

public class ChapterDetailItem
{
    public Guid Id { get; set; }

    public int RunningNumber { get; set; }

    public string ChapterName { get; set; } = string.Empty;

    public string ChapterDescription { get; set; } = string.Empty;

    public Guid ModuleId { get; set; }

    public List<ExerciseListItem> Exercises { get; set; } = new();

    
    public static ChapterDetailItem ToDetailItem(Chapter chapter)
    {
        return new ChapterDetailItem
        {
            Id = chapter.Id,
            ChapterDescription = chapter.ChapterDescription,
            ChapterName = chapter.ChapterName,
            ModuleId = chapter.ModuleId,
            RunningNumber = chapter.RunningNumber,
            Exercises = chapter.Exercises?
                .Select(e => ExerciseListItem.ToListItem(e))
                .OrderBy(e => e.Order)
                .ToList() ?? new List<ExerciseListItem>()
        };
    }
}