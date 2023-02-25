namespace Application.DTOs.ExercisesDTOs.Parson;

public class ParsonDetailItem : ExerciseDetailItem
{
    public List<ParsonExerciseLineDetailItem> ParsonLineList { get; set; }
}