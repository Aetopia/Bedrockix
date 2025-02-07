using System;
using System.Collections.Generic;
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

    static readonly IApplicationActivationManager ApplicationActivationManager;

    static readonly IPackageDebugSettings PackageDebugSettings;

    static App()
    {
        CoCreateInstance(ref CLSID_PackageDebugSettings, default, CLSCTX_INPROC_SERVER, ref IID_IPackageDebugSettings, out var ppv);
        PackageDebugSettings = (IPackageDebugSettings)ppv;

        CoCreateInstance(ref CLSID_ApplicationActivationManager, default, CLSCTX_INPROC_SERVER, ref IID_IApplicationActivationManager, out ppv);
        ApplicationActivationManager = (IApplicationActivationManager)ppv;
    }

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