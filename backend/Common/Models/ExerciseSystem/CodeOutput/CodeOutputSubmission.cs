using System.ComponentModel.DataAnnotations.Schema;
using Common.Models.Grading;
using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem.CodeOutput;

public class CodeOutputSubmission: BaseSubmission
{
    [Column(TypeName = "nvarchar(max)")]
    public string SubmittedAnswer { get; set; }
}
