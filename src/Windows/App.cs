using System;
using System.Threading;
using Bedrockix.Unmanaged.COM;
using Windows.ApplicationModel;
using System.Collections.Generic;
using Bedrockix.Unmanaged.Structures;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

public partial class App
{
    internal App(string @this)
    {
        ParseApplicationUserModelId(lstrcpy(Id, @this), packageFamilyName: Name);
        Info = new(() => AppInfo.GetFromAppUserModelId(@this), LazyThreadSafetyMode.PublicationOnly);
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
            nint @this = new(); APPLICATION_USER_MODEL_ID @params = new();
            while ((@this = FindWindowEx(hWndChildAfter: @this)) != default)
                using (Process @object = new(@this))
                {
                    if (GetApplicationUserModelId(@object.Handle, applicationUserModelId: @params)) continue;
                    else if (CompareStringOrdinal(Id, lpString2: @params) == CSTR_EQUAL) yield return @object.Id;
                }
        }
    }

    public partial bool Unpackaged => Package.IsDevelopmentMode;

    public partial bool Installed { get { GetPackagesByPackageFamily(Name, out var @this); return @this; } }

    public partial bool Debug
    {
        set
        {
            var @this = Package.Id.FullName;
            if (value) Settings.EnableDebugging(@this);
            else Settings.DisableDebugging(@this);
        }
    }

    public partial bool Running { get { foreach (var _ in Processes) return true; return false; } }

    public partial void Terminate() => Settings.TerminateAllProcesses(Package.Id.FullName);
}