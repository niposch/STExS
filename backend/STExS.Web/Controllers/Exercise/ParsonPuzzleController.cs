using Application.Services.Interfaces;
using Common.Models.ExerciseSystem.Parson;
using Microsoft.AspNetCore.Mvc;

namespace STExS.Controllers.Exercise;

[Route("api/[controller]")]
[ApiController]
public class ParsonPuzzleController: ControllerBase
{
    private readonly IParsonExerciseService parsonExerciseService;
    
    public ParsonPuzzleController(IParsonExerciseService parsonExerciseService)
    {
        this.parsonExerciseService = parsonExerciseService;
    }
}
