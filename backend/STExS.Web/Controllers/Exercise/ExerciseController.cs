using Application.Helper.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace STExS.Controllers.Exercise;

[ApiController]
[Route("api/[controller]")]
public class ExerciseController
{
    [Authorize(Roles = $"{RoleHelper.Admin}, {RoleHelper.Teacher}")]
    [HttpPost]
    public async Task<IActionResult> CopyToChapter([FromQuery] Guid existingExerciseId, [FromQuery] Guid chapterToCopyTo, CancellationToken cancellationToken = default)
    {
        
    }
    
    [HttpDelete]
    [Authorize(Roles = $"{RoleHelper.Admin}, {RoleHelper.Teacher}")]
    public async Task<IActionResult> Delete([FromQuery] Guid exerciseId, CancellationToken cancellationToken = default)
    {
        
    }
    
    [HttpPost("update")]
    public async Task<IActionResult> UpdateExercise([FromQuery] Guid chapterId, CancellationToken cancellationToken = default)
    {
        
    }
    
    [HttpGet]
    public Task<IActionResult> GetByChapterId([FromQuery] Guid chapterId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}