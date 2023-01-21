using Autofac;
using Common.ExternalInterface;

namespace External.Storage;

public class StorageModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<StorageDotNetFileStore>()
            .As<IFileStore>()
            .SingleInstance();
    }
}