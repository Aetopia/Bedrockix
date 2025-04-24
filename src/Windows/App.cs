using System;
using System.Threading;
using Bedrockix.Unmanaged.COM;
using Windows.ApplicationModel;
using System.Collections.Generic;
using Bedrockix.Unmanaged.Structures;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

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

    internal int Launch()
    {
        Manager.ActivateApplication(Id, default, ACTIVATEOPTIONS.AO_NOERRORUI, out var @this);
        return @this;
    }

    internal IEnumerable<int> Processes
    {
        get
        {
            nint hWnd = new(), hProcess = new();
            APPLICATION_USER_MODEL_ID @this = new();

            while ((hWnd = FindWindowEx(hWndChildAfter: hWnd)) != default)
            {
                try
                {
                    GetWindowThreadProcessId(hWnd, out var dwProcessId);
                    if (GetApplicationUserModelId(hProcess = OpenProcess(dwProcessId: dwProcessId), applicationUserModelId: @this)) continue;
                    else if (CompareStringOrdinal(Id, lpString2: @this) == CSTR_EQUAL) yield return dwProcessId;
                }
                finally { CloseHandle(hProcess); }
            }
        }
    }

    public partial bool Unpackaged => Package.IsDevelopmentMode;

    public partial bool Installed
    {
        get
        {
            GetPackagesByPackageFamily(Name, out var @this);
            return @this;
        }
    }

    public partial bool Debug
    {
        set
        {
            var @this = Package.Id.FullName;
            if (value) Settings.EnableDebugging(@this);
            else Settings.DisableDebugging(@this);
        }
    }

    public partial bool Running
    {
        get
        {
            foreach (var _ in Processes) return true;
            return false;
        }
    }

    public partial void Terminate() => Settings.TerminateAllProcesses(Package.Id.FullName);
}