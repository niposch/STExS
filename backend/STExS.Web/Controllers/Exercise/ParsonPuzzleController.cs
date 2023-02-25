using Application.DTOs.ExercisesDTOs.CodeOutput;
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
    public async Task<IActionResult> Create([FromBody] ParsonExerciseCreateDto parsonExerciseCreateDto)
    {
        /*
        var parsonExercise = await this.parsonExerciseService.CreateAsync(parsonExerciseCreateDto);
        return this.Ok(parsonExercise);
    */
        throw new NotImplementedException();
    }

    #endregion
}

public class ParsonExerciseCreateDto: BaseExerciseCreateItem
{
    public List<ParsonExerciseLineCreateItem> Lines { get; set; }
}

public class ParsonExerciseLineCreateItem
{
    public int Indentation { get; set; }
    public string Text { get; set; }
}
