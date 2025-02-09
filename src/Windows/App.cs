using System.Linq;
using Windows.ApplicationModel;
using Windows.System;
using static Bedrockix.Unmanaged.Constants;
using System.Threading;
using Bedrockix.Unmanaged;
using Windows.Foundation;

namespace Bedrockix.Windows;

sealed class App
{
    internal App(string value)
    {
        var source = AppDiagnosticInfo.RequestInfoForAppAsync(value);

        if (source.Status is AsyncStatus.Started)
        {
            using ManualResetEventSlim @event = new();
            source.Completed += (_, _) => @event.Set();
            @event.Wait();
        }

        if (source.Status is AsyncStatus.Error)
            throw source.ErrorCode;

        AppDiagnosticInfo = source.GetResults()[default];
    }

    readonly AppDiagnosticInfo AppDiagnosticInfo;

    static readonly ApplicationActivationManager ApplicationActivationManager = new();

    static readonly PackageDebugSettings PackageDebugSettings = new();

    internal Package Package => AppDiagnosticInfo.AppInfo.Package;

    internal bool Running => !AppDiagnosticInfo.GetResourceGroups().Any(_ => _.GetMemoryReport().PrivateCommitUsage is default(ulong));

    internal bool Debug
    {
        set
        {
            var packageFullName = Package.Id.FullName;
            PackageDebugSettings.DisableDebugging(packageFullName);
            if (!value) PackageDebugSettings.EnableDebugging(packageFullName, default, default);
        }
    }

    internal int Launch()
    {
        ApplicationActivationManager.ActivateApplication(AppDiagnosticInfo.AppInfo.AppUserModelId, default, AO_NOERRORUI, out var value);
        return value;
    }

    internal void Terminate() => PackageDebugSettings.TerminateAllProcesses(Package.Id.FullName);
}