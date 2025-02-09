using System.Linq;
using Windows.System;
using System.Threading;
using Windows.Foundation;
using Bedrockix.Unmanaged;
using Windows.ApplicationModel;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

sealed class App
{
    internal App(string value)
    {
        var @object = AppDiagnosticInfo.RequestInfoForAppAsync(value);

        if (@object.Status is AsyncStatus.Started)
        {
            using ManualResetEventSlim @event = new();
            @object.Completed += (_, _) => @event.Set();
            @event.Wait();
        }

        if (@object.Status is AsyncStatus.Error)
            throw @object.ErrorCode;

        AppDiagnosticInfo = @object.GetResults()[default];
    }

    readonly AppDiagnosticInfo AppDiagnosticInfo;

    static readonly ApplicationActivationManager ApplicationActivationManager = new();

    static readonly PackageDebugSettings PackageDebugSettings = new();

    internal Package Package => AppDiagnosticInfo.AppInfo.Package;

    internal bool Running => AppDiagnosticInfo.GetResourceGroups().Any(_ =>
    {
        var @object = _.GetMemoryReport();
        return @object != default && @object.PrivateCommitUsage != default;
    });

    internal bool Debug
    {
        set
        {
            var @object = Package.Id.FullName;
            if (value) PackageDebugSettings.EnableDebugging(@object, default, default);
            else PackageDebugSettings.DisableDebugging(@object);
        }
    }

    internal int Launch()
    {
        ApplicationActivationManager.ActivateApplication(AppDiagnosticInfo.AppInfo.AppUserModelId, default, AO_NOERRORUI, out var value);
        return value;
    }

    internal void Terminate(bool value = default)
    {
        var @object = Package.Id.FullName;
        if (value) PackageDebugSettings.TerminateAllProcesses(@object);
        else
        {
            try { PackageDebugSettings.StartServicing(@object); }
            finally { PackageDebugSettings.StartServicing(@object); }
        }
    }
}