using Common.Models.Grading;

namespace Common.Models.ExerciseSystem.Cloze;

public sealed class ClozeTextSubmission : BaseSubmission
{
    public List<ClozeTextAnswerItem> SubmittedAnswers { get; set; } = new();
}
