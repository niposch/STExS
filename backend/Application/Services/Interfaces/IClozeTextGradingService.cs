using Common.Models.ExerciseSystem.Cloze;

namespace Application.Services.Interfaces;

public interface IClozeTextGradingService
{
    Task GradeAsync(ClozeTextSubmission cloze);
}