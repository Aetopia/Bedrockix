using System;
using System.Linq;
using Windows.System;
using System.Threading;
using System.Collections;
using Windows.Foundation;
using Bedrockix.Unmanaged;
using Windows.ApplicationModel;
using System.Collections.Generic;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

sealed class App : IEnumerable<AppResourceGroupInfo>
{
    internal App(string value) => Object = new(() =>
    {
        var @this = AppDiagnosticInfo.RequestInfoForAppAsync(value);
        try
        {
            if (@this.Status is AsyncStatus.Started)
            {
                using ManualResetEventSlim @event = new();
                @this.Completed += (_, _) => @event.Set();
                @event.Wait();
            }

            if (@this.Status is AsyncStatus.Error) throw @this.ErrorCode;
            return @this.GetResults()[default];
        }
        finally { @this.Close(); }
    }, LazyThreadSafetyMode.PublicationOnly);

    readonly Lazy<AppDiagnosticInfo> Object;

    static readonly IApplicationActivationManager Manager = ApplicationActivationManager.Create();

    static readonly IPackageDebugSettings Settings = PackageDebugSettings.Create();

    internal Package Package => Object.Value.AppInfo.Package;

    internal bool Running => this.Any(_ => _.GetMemoryReport()?.PrivateCommitUsage > default(ulong));

    internal bool Debug
    {
        set
        {
            var name = Package.Id.FullName;
            if (value) Settings.EnableDebugging(name, default, default);
            else Settings.DisableDebugging(name);
        }
    }

    internal int Launch()
    {
        Manager.ActivateApplication(Object.Value.AppInfo.AppUserModelId, default, AO_NOERRORUI, out var value);
        return value;
    }

    internal void Terminate() => Settings.TerminateAllProcesses(Package.Id.FullName);

    public IEnumerator<AppResourceGroupInfo> GetEnumerator() => Object.Value.GetResourceGroups().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}