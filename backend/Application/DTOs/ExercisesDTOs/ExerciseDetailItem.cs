using Common.Models.ExerciseSystem;

namespace Application.DTOs.ExercisesDTOs;

public class ExerciseDetailItem
{
    public Guid Id { get; set; }
    public string ExerciseName { get; set; }
    public string ExerciseDescription { get; set; }
    public ExerciseType ExerciseType { get; set; }
    public int RunningNumber { get; set; }
    public int AchieveablePoints { get; set; }
    public Guid ChapterId { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
}