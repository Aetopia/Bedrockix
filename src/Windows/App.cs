using System;
using System.Threading;
using Windows.ApplicationModel;
using Bedrockix.Unmanaged.Types;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

unsafe sealed class App(string value)
{
    readonly Lazy<AppInfo> Object = new(() => AppInfo.GetFromAppUserModelId(value), LazyThreadSafetyMode.PublicationOnly);

    static readonly IApplicationActivationManager Manager = (IApplicationActivationManager)new ApplicationActivationManager();

    static readonly IPackageDebugSettings Settings = (IPackageDebugSettings)new PackageDebugSettings();

    internal string Id => Object.Value.AppUserModelId;

    internal Package Package => Object.Value.Package;

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
            fixed (char* lpString1 = Id)
            {
                nint hWnd = default, hProcess = default;
                var lpString2 = stackalloc char[APPLICATION_USER_MODEL_ID_MAX_LENGTH];

                while ((hWnd = FindWindowEx(hWndChildAfter: hWnd)) != default)
                    try
                    {
                        GetWindowThreadProcessId(hWnd, out var dwProcessId);
                        GetApplicationUserModelId(hProcess = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, false, dwProcessId), applicationUserModelId: lpString2);
                        if (CompareStringOrdinal(lpString1, lpString2: lpString2, bIgnoreCase: true) == CSTR_EQUAL) return true;
                    }
                    finally { CloseHandle(hProcess); }

                return false;
            }
        }
    }

    internal int Launch() { Manager.ActivateApplication(Id, default, AO_NOERRORUI, out var value); return value; }

    internal void Terminate() => Settings.TerminateAllProcesses(Package.Id.FullName);
}