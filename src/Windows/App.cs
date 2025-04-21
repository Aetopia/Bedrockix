using System;
using System.Threading;
using Windows.ApplicationModel;
using Bedrockix.Unmanaged.Types;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

unsafe sealed class App
{
    internal App(string value)
    {
        char* @this = stackalloc char[PACKAGE_RELATIVE_APPLICATION_ID_MAX_LENGTH], _ = stackalloc char[PACKAGE_RELATIVE_APPLICATION_ID_MAX_LENGTH];
        if (ParseApplicationUserModelId(value, packageFamilyName: @this, packageRelativeApplicationId: _)) throw new FormatException();
        this._ = new(@this); Object = new(() => AppInfo.GetFromAppUserModelId(value), LazyThreadSafetyMode.PublicationOnly);
    }

    public static implicit operator string(App @this) => @this.Object.Value.AppUserModelId;

    readonly string _; readonly Lazy<AppInfo> Object;

    static readonly IApplicationActivationManager Manager = (IApplicationActivationManager)new ApplicationActivationManager();

    static readonly IPackageDebugSettings Settings = (IPackageDebugSettings)new PackageDebugSettings();

    internal Package Package => Object.Value.Package;

    internal bool Installed { get { GetPackagesByPackageFamily(_, out var value); return value; } }

    internal bool Debug
    {
        set
        {
            var @this = Package.Id.FullName;
            if (value) Settings.EnableDebugging(@this, default, default);
            else Settings.DisableDebugging(@this);
        }
    }

    internal bool Running
    {
        get
        {
            fixed (char* @this = (string)this)
            {
                nint hWnd = default, hProcess = default;
                var _ = stackalloc char[APPLICATION_USER_MODEL_ID_MAX_LENGTH];

                while ((hWnd = FindWindowEx(hWndChildAfter: hWnd)) != default)
                    try
                    {
                        GetWindowThreadProcessId(hWnd, out var dwProcessId);
                        if (GetApplicationUserModelId(hProcess = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, dwProcessId: dwProcessId), applicationUserModelId: _)) continue;
                        else if (CompareStringOrdinal(@this, lpString2: _, bIgnoreCase: true) == CSTR_EQUAL) return true;
                    }
                    finally { CloseHandle(hProcess); }

                return false;
            }
        }
    }

    internal int Launch() { Manager.ActivateApplication(this, default, AO_NOERRORUI, out var value); return value; }

    internal void Terminate() => Settings.TerminateAllProcesses(Package.Id.FullName);
}