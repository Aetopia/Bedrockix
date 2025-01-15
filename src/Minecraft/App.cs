using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Bedrockix.Unmanaged;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.System;

namespace Bedrockix.Minecraft;
using static Native;

sealed class App(string value)
{
    static Guid Guid = new("B63EA76D-1F85-456F-A19C-48159EFA858B");

    readonly AppInfo AppInfo = AppInfo.GetFromAppUserModelId(value);

    static readonly PackageDebugSettings PackageDebugSettings = new();

    static readonly ApplicationActivationManager ApplicationActivationManager = new();

    internal string PackageFamilyName => AppInfo.PackageFamilyName;

    unsafe readonly struct _ : IDisposable
    {
        readonly nint Value;

        internal _(string value)
        {
            if (string.IsNullOrEmpty(value = value.Trim())) throw new ArgumentException();
            var _ = SHSimpleIDListFromPath(value); SHCreateShellItemArrayFromIDLists(1, &_, out Value);
        }

        readonly public void Dispose() { if (Value is not default(nint)) Marshal.Release(Value); }

        public static implicit operator nint(_ _) => _.Value;
    }

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

    internal int Launch()
    {
        PackageDebugSettings.EnableDebugging(AppInfo.Package.Id.FullName, default, default);
        ApplicationActivationManager.ActivateApplication(AppInfo.AppUserModelId, default, AO_NOERRORUI, out var processId);
        return processId;
    }

    internal int Protocol(string value)
    {
        using _ _ = new(value);
        PackageDebugSettings.EnableDebugging(AppInfo.Package.Id.FullName, default, default);
        ApplicationActivationManager.ActivateForProtocol(AppInfo.AppUserModelId, _, out var processId);
        return processId;
    }

    internal int File(string value)
    {
        using _ _ = new(value);
        PackageDebugSettings.EnableDebugging(AppInfo.Package.Id.FullName, default, default);
        ApplicationActivationManager.ActivateForFile(AppInfo.AppUserModelId, _, default, out var processId);
        return processId;
    }

    internal void Terminate() => PackageDebugSettings.TerminateAllProcesses(AppInfo.Package.Id.FullName);
}