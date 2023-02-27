namespace Application.DTOs.ExercisesDTOs.ClozeText;

public class ClozeTextExerciseCreateItem : BaseExerciseCreateItem
{
    // clozes are enclosed in 2 square brackets f.e. [[cloze]]
    // everything within those square brackets is url encoded
    // the answer is the text between the square brackets
    public string Text { get; set; }
}