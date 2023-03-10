using Common.Models.ExerciseSystem.Parson;

namespace Application.Services.Interfaces;

public interface IParsonGradingService
{
    Task GradeAsync(ParsonPuzzleSubmission parson);
}