using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Interfaces;
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
        List<Guid> linesInSubmittedOrder,
        Guid timeTrackId,
        CancellationToken cancellationToken = default)
    {
        var parsonElements = await this.repository.ParsonElements.GetForExerciseAsync(exerciseId, cancellationToken);

        this.ValidateSubmittedAnswer(linesInSubmittedOrder, parsonElements);

        var elementsDict = parsonElements.ToDictionary(e => e.Id);
        
        var parsonPuzzleSubmission = new ParsonPuzzleSubmission
        {
            Id = Guid.NewGuid(),
            ParsonElements = linesInSubmittedOrder
                .Select(id => elementsDict[id])
                .ToList(),
            UserId = userId,
            ExerciseId = exerciseId
        };
        
        await this.submissionService.SubmitAsync(userId, exerciseId, parsonPuzzleSubmission, isFinal, timeTrackId, cancellationToken);
    }

    private void ValidateSubmittedAnswer(List<Guid> submittedAnswer, List<ParsonElement> parsonElements)
    {
        if (submittedAnswer.Count != parsonElements.Count)
        {
            throw new ArgumentException("Submitted answer does not contain the same number of lines as the expected solution.");
        }

        if(!submittedAnswer.ToHashSet()
            .SetEquals(parsonElements.Select(e => e.Id).ToHashSet())){
            throw new ArgumentException("Submitted answer does not contain the same lines as the expected solution.");
        }
    }
}