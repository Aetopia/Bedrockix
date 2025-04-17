using System;
using System.Linq;
using Windows.System;
using System.Threading;
using System.Collections;
using Windows.Foundation;
using Windows.ApplicationModel;
using Bedrockix.Unmanaged.Types;
using System.Collections.Generic;
using Windows.Management.Deployment;
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

    static readonly PackageManager PackageManager = new();

    static readonly IApplicationActivationManager ApplicationActivationManager = (IApplicationActivationManager)new ApplicationActivationManager();

    static readonly IPackageDebugSettings PackageDebugSettings = (IPackageDebugSettings)new PackageDebugSettings();

    internal Package Package => Object.Value.AppInfo.Package;

    internal bool Running => this.Any(_ => _.GetProcessDiagnosticInfos().Count != default);

    internal bool Installed => PackageManager.FindPackagesForUser(string.Empty, Value).Any();

    internal IEnumerable<System.Diagnostics.Process> Processes => this.SelectMany(_ => _.GetProcessDiagnosticInfos().Select(_ => System.Diagnostics.Process.GetProcessById((int)_.ProcessId)));

    internal bool Debug
    {
        set
        {
            var @this = Package.Id.FullName;
            if (value) PackageDebugSettings.EnableDebugging(@this, default, default);
            else PackageDebugSettings.DisableDebugging(@this);
        }
    }

    internal int Launch()
    {
        ApplicationActivationManager.ActivateApplication(Object.Value.AppInfo.AppUserModelId, default, AO_NOERRORUI, out var value);
        return value;
    }

    internal void Terminate() => PackageDebugSettings.TerminateAllProcesses(Package.Id.FullName);

    public IEnumerator<AppResourceGroupInfo> GetEnumerator() => Object.Value.GetResourceGroups().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}