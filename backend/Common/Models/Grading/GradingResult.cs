﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.Grading;

public class GradingResult
{
    public Guid Id { get; set; }
    
    public string Comment { get; set; }
    public int Points { get; set; }
    
    public GradingState GradingState { get; set; }
    
    public bool IsAutomaticallyGraded { get; set; }
    
    public DateTime? AppealDate { get; set; }
    
    public DateTime FinalAppealDate { get; set; }
    
    [NotMapped]
    public bool IsAppealed => AppealDate != null;
    
    public DateTime CreationDate { get; set; }
    
    public Guid? GradedSubmissionId { get; set; }
    public BaseSubmission? GradedSubmission { get; set; }
    
}

public enum GradingState
{
    Unreviewed,
    InProgress,
    Graded,
    Appealed,
    AppealAccepted,
    AppealRejected,
    FinallyGraded
}