namespace Application.DTOs.ExercisesDTOs.ClozeText;

public class ClozeTextExerciseCreateItem // TODO make this class inherit from ExerciseCreateItem
{
    // clozes are enclosed in 2 square brackets f.e. [[cloze]]
    // everything within those square brackets is url encoded
    // the answer is the text between the square brackets
    public string Text { get; set; }

    public string ExerciseName { get; set; }

    public string ExerciseDescription { get; set; }

    public int AchievablePoints { get; set; }

    public Guid ChapterId { get; set; }
}