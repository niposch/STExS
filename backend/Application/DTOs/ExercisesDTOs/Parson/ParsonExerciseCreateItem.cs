namespace Application.DTOs.ExercisesDTOs.Parson;

public class ParsonExerciseCreateItem : BaseExerciseCreateItem
{
    public List<ParsonExerciseLineCreateItem> Lines { get; set; }

    public bool IndentationIsRelevant { get; set; }
}