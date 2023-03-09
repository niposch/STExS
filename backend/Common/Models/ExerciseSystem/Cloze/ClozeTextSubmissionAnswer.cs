namespace Common.Models.ExerciseSystem.Cloze;
public class ClozeTextAnswerItem
{
    public string SubmittedAnswer { get; set; } = string.Empty;

    public int Index { get; set; }

    public Guid ClozeTextSubmissionId { get; set; }

    public ClozeTextSubmission ClozeTextSubmission { get; set; } = null!;
}