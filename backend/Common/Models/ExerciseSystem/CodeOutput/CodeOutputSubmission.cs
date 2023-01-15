using System.ComponentModel.DataAnnotations.Schema;
using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem.CodeOutput;

public class CodeOutputSubmission: DeletableBaseEntity
{
    public CodeOutputExercise CodeOutputExercise { get; set; }
    
    [Column(TypeName = "nvarchar(max)")]
    public string SubmittedAnswer { get; set; }
    
    public SubmissionState IsInprogress { get; set; }
    
    public TimeSpan TimeTaken { get; set; }
    
}

public enum SubmissionState
{
    InProgress = 1,
    Submitted = 2,
    
    // following states will be used by the grading system
    Correct = 3,
    Incorrect = 4
}