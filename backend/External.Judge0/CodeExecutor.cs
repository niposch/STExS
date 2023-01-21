using Common.ExternalInterface;
using Judge0;

namespace External.Judge0;

public class CodeExecutor: ICodeExecutor
{
    private readonly IJudge0Service judge0Service;
    public CodeExecutor()
    {
        var client = new HttpClient();
        this.judge0Service = new Judge0Service(client);
    }

    public async Task<ExecutionResult> ExecuteAsync(ExecutableProjectInformation code, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}