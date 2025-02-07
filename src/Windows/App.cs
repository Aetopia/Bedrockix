using Bedrockix.Unmanaged;
using Windows.ApplicationModel;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

sealed class App
{
    readonly AppInfo AppInfo;

    static readonly IApplicationActivationManager ApplicationActivationManager = Components.ApplicationActivationManager.Create();

    static readonly IPackageDebugSettings PackageDebugSettings = Components.PackageDebugSettings.Create();

    internal App(string value) => AppInfo = AppInfo.GetFromAppUserModelId(value);

    internal Package Package => AppInfo.Package;

    internal bool Running => PackageDebugSettings.Resume(Package.Id.FullName) >= default(int);

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
        ApplicationActivationManager.ActivateApplication(AppInfo.AppUserModelId, default, AO_NOERRORUI, out var processId);
        return processId;
    }

    internal void Terminate() => PackageDebugSettings.TerminateAllProcesses(Package.Id.FullName);
}