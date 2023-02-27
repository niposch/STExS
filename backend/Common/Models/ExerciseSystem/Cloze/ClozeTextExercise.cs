using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.ExerciseSystem.Cloze;

public sealed class ClozeTextExercise : BaseExercise
{
    [Column(TypeName = "nvarchar(max)")]
    public string TextWithAnswers { get; set; } = string.Empty;
}