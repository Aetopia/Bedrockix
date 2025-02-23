using System.Linq;
using Windows.System;
using System.Threading;
using System.Collections;
using Windows.Foundation;
using Windows.ApplicationModel;
using System.Collections.Generic;
using Bedrockix.Unmanaged.Components;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

sealed class App : IEnumerable<AppResourceGroupInfo>
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

            if (@object.Status is AsyncStatus.Error) throw @object.ErrorCode;

            Object = @object.GetResults()[default];
        }
        finally { @object.Close(); }
    }

    readonly AppDiagnosticInfo Object;

    static readonly ApplicationActivationManager Manager = new();

    static readonly PackageDebugSettings Settings = new();

    internal Package Package => Object.AppInfo.Package;

    internal bool Running => this.Any(_ => _.GetMemoryReport()?.PrivateCommitUsage > default(ulong));

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
        Manager.ActivateApplication(Object.AppInfo.AppUserModelId, default, AO_NOERRORUI, out var value);
        return value;
    }

    internal void Terminate() => Settings.TerminateAllProcesses(Package.Id.FullName);

    public IEnumerator<AppResourceGroupInfo> GetEnumerator() => Object.GetResourceGroups().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}