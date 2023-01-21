namespace Common.ExternalInterface;


public enum ExecutionResultType
{
    WaitingForStart = 1,
    Running = 2,
    Failed = 3,
    Passed = 4,
    NoTests = 5
}

public sealed class ExecutionResult
{
    public ExecutionResultType ExecutionResultType { get; set; }
    public DateTime ExecutionStartDate { get; set; }
    public DateTime? ExecutionFinishedDate { get; set; }
    public string ExecutionOutputId { get; set; } // go look for this in the blob storage service
}

public enum ProjectType
{
    Python = 1,
    Java = 2
}

public sealed class ExecutableProjectInformation
{
    public ProjectType ProjectType { get; set; }
    
    public string ProjectRootDir { get; set; } // see the storage service
}

public interface ICodeExecutor
{
    public Task<ExecutionResult> ExecuteAsync(ExecutableProjectInformation code, CancellationToken cancellationToken = default);
}