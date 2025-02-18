using System.Linq;
using Windows.System;
using System.Threading;
using Windows.Foundation;
using Windows.ApplicationModel;
using static Bedrockix.Unmanaged.Constants;
using Bedrockix.Unmanaged;

namespace Bedrockix.Windows;

sealed class App
{
    internal App(string value)
    {
        var @object = AppDiagnosticInfo.RequestInfoForAppAsync(value);
        try
        {
            if (@object.Status is AsyncStatus.Started)
            {
                using ManualResetEventSlim @event = new();
                @object.Completed += (_, _) => @event.Set();
                @event.Wait();
            }

            if (@object.Status is AsyncStatus.Error)
                throw @object.ErrorCode;

            Info = @object.GetResults()[default];
        }
        finally { @object.Close(); }
    }

    internal readonly AppDiagnosticInfo Info;

    static readonly ApplicationActivationManager Manager = new();

    static readonly PackageDebugSettings Settings = new();

    internal Package Package => Info.AppInfo.Package;

    internal bool Running => Info.GetResourceGroups().Any(_ =>
    {
        var @object = _.GetMemoryReport();
        return @object != default && @object.PrivateCommitUsage != default;
    });

    internal bool Debug
    {
        set
        {
            var @object = Package.Id.FullName;
            if (value) Settings.EnableDebugging(@object, default, default);
            else Settings.DisableDebugging(@object);
        }
    }

    internal int Launch()
    {
        Manager.ActivateApplication(Info.AppInfo.AppUserModelId, default, AO_NOERRORUI, out var value);
        return value;
    }

    internal void Terminate() => Settings.TerminateAllProcesses(Package.Id.FullName);
}