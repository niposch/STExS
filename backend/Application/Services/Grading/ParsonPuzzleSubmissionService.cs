using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.ExercisesDTOs.Parson;
using Application.Services.Interfaces;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.Parson;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services.Grading;
public class ParsonPuzzleSubmissionService: IParsonPuzzleSubmissionService
{
        private readonly IApplicationRepository repository;
    
    private readonly ITimeTrackService timeTrackService;
    private readonly ISubmissionService submissionService;

    public ParsonPuzzleSubmissionService(IApplicationRepository repository, ITimeTrackService timeTrackService, ISubmissionService submissionService)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.timeTrackService = timeTrackService ?? throw new ArgumentNullException(nameof(timeTrackService));
        this.submissionService = submissionService;
    }

    public async Task SubmitAsync(Guid userId,
        Guid exerciseId,
        bool isFinal,
        List<ParsonExerciseLineDetailItem> linesInSubmittedOrder,
        Guid timeTrackId,
        CancellationToken cancellationToken = default)
    {
        var parsonElements = await this.repository.ParsonElements.GetForExerciseAsync(exerciseId, cancellationToken);

        if (isFinal)
            this.ValidateSubmittedAnswer(linesInSubmittedOrder, parsonElements);

        var elementsDict = linesInSubmittedOrder.ToDictionary(
            element => element.Id, 
            element => parsonElements.Find(e => element.Id.Equals(e.Id))
            );

        var submissionId = Guid.NewGuid();
        var parsonPuzzleSubmission = new ParsonPuzzleSubmission
        {
            Id = submissionId,
            AnswerItems = linesInSubmittedOrder
                .Select(detailItem =>
                new {
                    ParsonElement = elementsDict[detailItem.Id],
                    DetailItem = detailItem
                        
                })
                .Where(el => el.ParsonElement != null)
                .Select((el,i) =>
                new ParsonPuzzleAnswerItem{
                    ParsonElementId = el.ParsonElement!.Id,
                    SubmissionId = submissionId,
                    Indentation = el.DetailItem.Indentation,
                    RunningNumber = i
                })
                .ToList(),
            UserId = userId,
            ExerciseId = exerciseId,
            SubmissionType = ExerciseType.Parson
        };
        
        await this.submissionService.SubmitAsync(userId, exerciseId, parsonPuzzleSubmission, isFinal, timeTrackId, cancellationToken);
    }

    private void ValidateSubmittedAnswer(List<ParsonExerciseLineDetailItem> submittedAnswer, List<ParsonElement> parsonElements)
    {
        if (submittedAnswer.Count != parsonElements.Count)
        {
            throw new ArgumentException("Submitted answer does not contain the same number of lines as the expected solution.");
        }

        if(!submittedAnswer.Select(s => s.Id).ToHashSet()
            .SetEquals(parsonElements.Select(e => e.Id).ToHashSet())){
            throw new ArgumentException("Submitted answer does not contain the same lines as the expected solution.");
        }
    }
}