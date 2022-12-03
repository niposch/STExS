using Application.Services.Interfaces;
using Common.Models.ExerciseSystem.Parson;
using Microsoft.AspNetCore.Mvc;

namespace STExS.Controllers.Exercise;

[Route("api/[controller]")]
[ApiController]
public class ParsonPuzzleController: ControllerBase
{
    private readonly IParsonPuzzleService parsonPuzzleService;
    
    public ParsonPuzzleController(IParsonPuzzleService parsonPuzzleService)
    {
        this.parsonPuzzleService = parsonPuzzleService;
    }
    
    [HttpGet("getAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ParsonExercise>))]
    public async Task<IActionResult> GetAll()
    {
        var parsonPuzzles = await this.parsonPuzzleService.GetAllAsync();
        return Ok(parsonPuzzles);
    }
}
