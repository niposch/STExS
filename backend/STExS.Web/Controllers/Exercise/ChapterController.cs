using Application.DTOs.ChapterDTOs;
using Application.DTOs.ExercisesDTOs;
using Application.Helper.Roles;
using Application.Services.Interfaces;
using Common.Models.ExerciseSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STExS.Helper;

namespace STExS.Controllers.Exercise;

[ApiController]
[Route("api/[controller]")]
public class ChapterController: ControllerBase
{
    private readonly IChapterService chapterService;
    
    public ChapterController(IChapterService chapterService)
    {
        this.chapterService = chapterService ?? throw new ArgumentNullException(nameof(chapterService));
    }
    
    // write routes for crud  and for getting all chapters for a course and for getting all chapters
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChapterDetailItem))]
    [Authorize]
    public async Task<IActionResult> GetChapterDetail([FromQuery] Guid chapterId, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        var chapter = await this.chapterService.GetChapterAsync(chapterId, userId, cancellationToken);
        return Ok(ChapterDetailItem.ToDetailItem(chapter));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [Authorize]
    public async Task<IActionResult> CreateChapter([FromBody] ChapterCreateItem chapter, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        var chapterId = await this.chapterService.CreateChapterAsync(chapter.ModuleId,
            chapter.ChapterName,
            chapter.ChapterDescription,
            userId,
            cancellationToken);
        return Ok(chapterId);
    }
    
    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IActionResult> UpdateChapter([FromBody] ChapterUpdateItem chapter, [FromQuery] Guid chapterId, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        await this.chapterService.UpdateChapterAsync(chapterId,
            chapter.ChapterName,
            chapter.ChapterDescription,
            userId,
            cancellationToken);
        return Ok();
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IActionResult> DeleteChapter([FromQuery] Guid chapterId, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        await this.chapterService.DeleteChapterAsync(chapterId, userId, cancellationToken);
        return Ok();
    }
    
    [HttpGet("forModule")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChapterDetailItem>))]
    [Authorize]
    public async Task<IActionResult> GetChaptersForModule([FromQuery] Guid moduleId, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        var chapters = await this.chapterService.GetChaptersByModuleIdAsync(moduleId, userId, cancellationToken);
        return Ok(chapters.Select(c => ChapterDetailItem.ToDetailItem(c)).ToList());
    }
    
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChapterDetailItem>))]
    [Authorize]
    public async Task<IActionResult> GetAllChapters(CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        var chapters = await this.chapterService.GetAllChaptersAsync(userId, cancellationToken);
        return Ok(chapters.Select(c => ChapterDetailItem.ToDetailItem(c)).ToList());
    }
    
    public class ReorderChaptersRequest
    {
        public Guid ModuleId { get; set; }
        public List<Guid> ChapterIds { get; set; }
    }
    [HttpPost("reorder")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(Roles = $"{RoleHelper.Admin},{RoleHelper.Teacher}")]
    public async Task<IActionResult> ReorderChapters([FromBody] ReorderChaptersRequest request, CancellationToken cancellationToken = default)
    {
        var userId = this.User.GetUserId();
        await this.chapterService.ReorderModuleChaptersAsync(request.ChapterIds, request.ModuleId, userId, cancellationToken);
        return Ok();
    }
}