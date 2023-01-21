using Autofac;
using Common.ExternalInterface;

namespace External.Judge0;

public class Judge0Module:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CodeExecutor>()
            .As<ICodeExecutor>()
            .SingleInstance();
    }
}