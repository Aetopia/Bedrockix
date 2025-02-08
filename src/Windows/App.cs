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
    readonly AppInfo AppInfo;

    internal App(string value) => AppInfo = AppInfo.GetFromAppUserModelId(value);

    static readonly ApplicationActivationManager ApplicationActivationManager = new();

    static readonly PackageDebugSettings PackageDebugSettings =new();

    internal Package Package => AppInfo.Package;

    internal bool Running
    {
        get
        {
            var source = AppDiagnosticInfo.RequestInfoForAppAsync(AppInfo.AppUserModelId);

            if (source.Status is AsyncStatus.Started)
            {
                using ManualResetEventSlim @event = new();
                source.Completed += (_, _) => @event.Set();
                @event.Wait();
            }

            if (source.Status is AsyncStatus.Error)
                throw source.ErrorCode;

            return source.GetResults().Any(_ => _.GetResourceGroups().Any(_ => _.GetProcessDiagnosticInfos().Any()));
        }
    }

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
        ApplicationActivationManager.ActivateApplication(AppInfo.AppUserModelId, default, AO_NOERRORUI, out var value);
        return value;
    }

    internal void Terminate() => PackageDebugSettings.TerminateAllProcesses(Package.Id.FullName);
};