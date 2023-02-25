using Application.DTOs.ExercisesDTOs.CodeOutput;
using Application.DTOs.ExercisesDTOs.Parson;
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

    #region Administration

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] ParsonExerciseCreateItem parsonExerciseCreateItem)
    {
        /*
        var parsonExercise = await this.parsonExerciseService.CreateAsync(parsonExerciseCreateDto);
        return this.Ok(parsonExercise);
    */
        throw new NotImplementedException();
    }

    #endregion
}