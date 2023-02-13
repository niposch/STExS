namespace Common.Models.Grading;

public class GradingResult
{
    public Guid Id { get; set; }
    
    public string Comment { get; set; }
    public int Points { get; set; }
    public bool IsFinal { get; set; }
    public bool IsAutomatic { get; set; }
    public DateTime? AppealDate { get; set; }
    
    public Guid UserSubmissionId { get; set; }
    public UserSubmission UserSubmission { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public Guid? GradedSubmissionId { get; set; }
    public BaseSubmission? GradedSubmission { get; set; }
    
}