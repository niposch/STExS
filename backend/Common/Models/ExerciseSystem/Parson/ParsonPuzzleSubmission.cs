using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models.Grading;

namespace Common.Models.ExerciseSystem.Parson;
public class ParsonPuzzleSubmission: BaseSubmission
{
    public List<ParsonElement> ParsonElements { get; set; } = new();
}

public class ParsonPuzzleSubmittedElement
{
    public ParsonElement ParsonElement { get; set; } = null!;
    public int RunningNumber { get; set; }
    public ParsonPuzzleSubmission ParsonPuzzleSubmission { get; set; } = null!;
    public Guid ParsonPuzzleSubmissionId { get; set; }
}