using System.ComponentModel.DataAnnotations.Schema;
using Application.DTOs;
using Application.Services.Grading;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using STExS.Helper;
using Application.DTOs.ExercisesDTOs;
using Application.DTOs.GradingReportDTOs;
using Application.DTOs.ModuleDTOs;
using Application.Services.Interfaces;
using Common.Models.Grading;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using STExS.Controllers.Exercise;

namespace STExS.Controllers.Grading;

[ApiController]
[Route("api/[controller]")]
public class GradingController : ControllerBase
{
    private readonly IGradingService gradingService;

    private readonly ISubmissionService submissionService;

    private readonly IAccessService accessService;

    public GradingController(IGradingService gradingService, IAccessService accessService, ISubmissionService submissionService)
    {
        this.gradingService = gradingService ?? throw new ArgumentNullException(nameof(gradingService));
        this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
        this.submissionService = submissionService ?? throw new ArgumentNullException(nameof(submissionService));
    }

    [HttpGet("module")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModuleReport))]
    public async Task<IActionResult> GetModuleReport([FromQuery] Guid moduleId, CancellationToken cancellationToken = default)
    {
        return this.Ok(await this.gradingService.GetModuleReportAsync(moduleId, cancellationToken));
    }

    [HttpGet("exercise")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExerciseReportItem>))]
    public async Task<IActionResult> GetExerciseReport([FromQuery] Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var res = await this.gradingService.GetExerciseReportAsync(exerciseId, cancellationToken);

        return this.Ok(res);
    }
    
    [HttpGet("gradingForSubmission")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GradingResultDetailItem))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGradingReport([FromQuery] Guid submissionId, CancellationToken cancellationToken = default)
    {
        var res = await this.gradingService.GetGradingResultForSubmissionAsync(submissionId, cancellationToken);
        if (res == null)
        {
            return this.NotFound();
        }

        var detailItem = new GradingResultDetailItem
        {
            Points = res.Points,
            Id = res.Id,
            Comment = res.Comment,
            AppealableBefore = res.AppealableBefore,
            GradingState = res.GradingState,
            AppealDate = res.AppealDate,
            CreationDate = res.CreationDate,
            AutomaticGradingDate = res.AutomaticGradingDate,
            GradedSubmissionId = res.GradedSubmissionId,
            IsAutomaticallyGraded = res.IsAutomaticallyGraded,
            ManualGradingDate = res.ManualGradingDate
        };
        
        return this.Ok(detailItem);
    }
    
    [HttpPost("manualGrading")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GradeExercise(Guid submissionId, int newGrade, string? comment = null, CancellationToken cancellationToken = default)
    {
        comment ??= string.Empty;
        await this.gradingService.ManuallyGradeExerciseAsync(submissionId, newGrade, comment, this.User.GetUserId(), cancellationToken);
        return this.Ok();
    }
    
    [HttpGet("submissionsForUserAndExercise")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SubmissionDetailItem>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSubmissionsForUserAndExercise(Guid exerciseId, Guid userId, CancellationToken cancellationToken = default)
    {
        if (!await this.accessService.IsExerciseAdminAsync(exerciseId, this.User.GetUserId(), cancellationToken))
            return this.StatusCode(StatusCodes.Status401Unauthorized);
        
        var res = await this.submissionService.GetSubmissionsForUserAndExerciseAsync(exerciseId, userId, cancellationToken);
        return this.Ok(res);
    }
}

public class GradingResultDetailItem
{
    public Guid Id { get; set; }
    
    public string Comment { get; set; }
    public int Points { get; set; }
    
    public GradingState GradingState { get; set; }
    
    public bool IsAutomaticallyGraded { get; set; }
    
    public DateTime? AppealDate { get; set; }
    
    public DateTime? AppealableBefore { get; set; }
    
    [NotMapped]
    public bool IsAppealed => AppealDate != null;
    
    public DateTime CreationDate { get; set; }
    
    public Guid? GradedSubmissionId { get; set; }
    
    public DateTime? AutomaticGradingDate { get; set; }
    
    public DateTime? ManualGradingDate { get; set; }
}