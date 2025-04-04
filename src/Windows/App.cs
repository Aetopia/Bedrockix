using System;
using System.Linq;
using Windows.System;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using Windows.Foundation;
using Bedrockix.Unmanaged;
using Windows.ApplicationModel;
using System.Collections.Generic;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

sealed class App : IEnumerable<AppResourceGroupInfo>
{
    internal App(string value)
    {
        Value = value;
        Object = new(() =>
        {
            var @this = AppDiagnosticInfo.RequestInfoForPackageAsync(value);
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
    }

    readonly string Value;

    readonly Lazy<AppDiagnosticInfo> Object;

    static readonly IApplicationActivationManager Manager = ApplicationActivationManager.Create();

    static readonly IPackageDebugSettings Settings = PackageDebugSettings.Create();

    internal Package Package => Object.Value.AppInfo.Package;

    internal bool Running => this.Any(_ => _.GetProcessDiagnosticInfos().Count != default);

    internal bool Installed => GetPackagesByPackageFamily(Value, out _, default, out _, default) is ERROR_INSUFFICIENT_BUFFER;

    internal IEnumerable<Process> Processes => this.SelectMany(_ => _.GetProcessDiagnosticInfos().Select(_ => Process.GetProcessById((int)_.ProcessId)));

    internal bool Debug
    {
        set
        {
            var @this = Package.Id.FullName;
            if (value) Settings.EnableDebugging(@this, default, default);
            else Settings.DisableDebugging(@this);
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