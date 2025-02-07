using System;
using System.Linq;
using System.Runtime.Hosting;
using System.Threading;
using Bedrockix.Unmanaged;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.System;
using static Bedrockix.Unmanaged.Native;

namespace Bedrockix.Windows;

sealed class App
{
    readonly AppInfo AppInfo;

    static readonly ApplicationActivationManager ApplicationActivationManager = new();

    static readonly PackageDebugSettings PackageDebugSettings = new();

    internal App(string value) => AppInfo = AppInfo.GetFromAppUserModelId(value);

    internal Package Package => AppInfo.Package;

    internal bool Running
    {
        get
        {
            var _ = AppDiagnosticInfo.RequestInfoForAppAsync(AppInfo.AppUserModelId);
            if (_.Status is AsyncStatus.Started) using (ManualResetEventSlim @event = new()) { _.Completed += (_, _) => @event.Set(); @event.Wait(); }
            try { return _.Status is AsyncStatus.Error ? throw _.ErrorCode : _.GetResults().Any(_ => _.GetResourceGroups().SelectMany(_ => _.GetProcessDiagnosticInfos()).Any()); }
            finally { _.Close(); }
        }
    }

    internal bool Lifecycle
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
        ApplicationActivationManager.ActivateApplication(AppInfo.AppUserModelId, default, AO_NOERRORUI, out var processId);
        return processId;
    }

    internal void Terminate() => PackageDebugSettings.TerminateAllProcesses(Package.Id.FullName);
}