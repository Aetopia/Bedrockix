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

/// <summary>
/// Represents a packaged application and provides methods for interaction.
/// </summary>
sealed class App
{
    readonly AppInfo AppInfo;

    static readonly ApplicationActivationManager ApplicationActivationManager = new();

    static readonly PackageDebugSettings PackageDebugSettings = new();

    /// <summary>
    /// Initializes a new instance of the class using the specified AUMID.
    /// </summary>
    /// <param name="value">The AUMID.</param>
    internal App(string value) => AppInfo = AppInfo.GetFromAppUserModelId(value);

    /// <summary>
    /// Gets the associated package. 
    /// </summary>
    internal Package Package => AppInfo.Package;


    /// <summary>
    /// Gets a value indicating if an application is running or not.
    /// </summary>
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

    /// <summary>
    /// Configure if an application has its lifecycle managed by PLM.
    /// </summary>
    internal bool Lifecycle
    {
        set
        {
            var packageFullName = Package.Id.FullName;
            PackageDebugSettings.DisableDebugging(packageFullName);
            if (!value) PackageDebugSettings.EnableDebugging(packageFullName, default, default);
        }
    }

    /// <summary>
    /// Launches the application.
    /// </summary>
    /// <returns>The PID of the launched application</returns>
    internal int Launch()
    {
        ApplicationActivationManager.ActivateApplication(AppInfo.AppUserModelId, default, AO_NOERRORUI, out var processId);
        return processId;
    }

    /// <summary>
    /// Terminates all processes associated with the application's package. 
    /// </summary>
    internal void Terminate() => PackageDebugSettings.TerminateAllProcesses(Package.Id.FullName);
}