using System.ComponentModel.DataAnnotations.Schema;
using Common.Models.ExerciseSystem;

namespace Application.DTOs.ExercisesDTOs;

public class ExerciseDetailItem
{
    public Guid Id { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public string ExerciseDescription { get; set; } = string.Empty;
    public ExerciseType ExerciseType { get; set; }
    public int RunningNumber { get; set; }
    public int AchievablePoints { get; set; }
    public Guid ChapterId { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }

    // whether the user has submitted a solution for this exercise
    [NotMapped]
    public bool? UserHasSolvedExercise { get; set; }
}