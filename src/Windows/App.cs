using System;
using System.Threading;
using Bedrockix.Unmanaged.COM;
using Windows.ApplicationModel;
using System.Collections.Generic;
using Bedrockix.Unmanaged.Structures;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;
using System.Linq;

namespace Bedrockix.Windows;

public unsafe partial class App
{
    internal App(string value)
    {
        ParseApplicationUserModelId(lstrcpy(Id, value), packageFamilyName: Name);
        Info = new(() => AppInfo.GetFromAppUserModelId(value), LazyThreadSafetyMode.PublicationOnly);
    }

    readonly Lazy<AppInfo> Info;

    readonly PACKAGE_FAMILY_NAME Name = new();

    readonly APPLICATION_USER_MODEL_ID Id = new();

    internal Package Package => Info.Value.Package;

    static readonly IPackageDebugSettings Settings = (IPackageDebugSettings)new PackageDebugSettings();

    static readonly IApplicationActivationManager Manager = (IApplicationActivationManager)new ApplicationActivationManager();

    internal int Launch() { Manager.ActivateApplication(Id, default, ACTIVATEOPTIONS.AO_NOERRORUI, out var value); return value; }

    internal IEnumerable<int> Processes
    {
        get
        {
            nint hWnd = new(), hProcess = new();
            APPLICATION_USER_MODEL_ID value = new();

            while ((hWnd = FindWindowEx(hWndChildAfter: hWnd)) != default)
            {
                try
                {
                    GetWindowThreadProcessId(hWnd, out var dwProcessId);
                    hProcess = OpenProcess(dwProcessId: dwProcessId);

                    if (GetApplicationUserModelId( hProcess, applicationUserModelId: value)) continue;
                    else if (CompareStringOrdinal(Id, lpString2: value) == CSTR_EQUAL) yield return dwProcessId;
                }
                finally { CloseHandle(hProcess); }
            }
        }
    }

    public partial bool Unpackaged => Package.IsDevelopmentMode;

    public partial bool Installed { get { GetPackagesByPackageFamily(Name, out var value); return value; } }

    public partial bool Debug
    {
        set
        {
            var @this = Package.Id.FullName;
            if (value) Settings.EnableDebugging(@this, new(), new());
            else Settings.DisableDebugging(@this);
        }
    }

    public partial bool Running => Processes.Any();

    public partial void Terminate() => Settings.TerminateAllProcesses(Package.Id.FullName);
}