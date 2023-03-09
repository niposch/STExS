using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models.Grading;

namespace Common.Models.ExerciseSystem.Parson;
public class ParsonPuzzleSubmission: BaseSubmission
{
    public List<ParsonPuzzleAnswerItem> AnswerItems { get; set; } = new();
}

public class ParsonPuzzleAnswerItem
{
    public Guid Id { get; set; }
    public ParsonPuzzleSubmission Submission { get; set; }
    
    public Guid SubmissionId { get; set; }
    
    public Guid ParsonElementId { get; set; }
    
    public ParsonElement ParsonElement { get; set; } = null!;
    
    public int RunningNumber { get; set; }
    
    public int Indentation { get; set; }
}