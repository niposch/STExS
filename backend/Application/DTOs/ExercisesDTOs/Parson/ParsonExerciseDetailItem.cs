namespace Application.DTOs.ExercisesDTOs.Parson;

public class ParsonExerciseDetailItem : ExerciseDetailItem
{
    public List<ParsonExerciseLineDetailItem> Lines { get; set; }
    public bool IndentationIsRelevant { get; set; }
}