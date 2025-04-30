using System;
using System.Threading;
using Windows.ApplicationModel;
using System.Collections.Generic;
using Bedrockix.Unmanaged;
using static Bedrockix.Unmanaged.Safe;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

public partial class App
{
    internal App(string @this)
    {
        Id = new(@this); Name = ParseApplicationUserModelId(Id);
        Info = new(() => AppInfo.GetFromAppUserModelId(@this), LazyThreadSafetyMode.PublicationOnly);
    }

    readonly ApplicationUserModelId Id; readonly PackageFamilyName Name;

    readonly Lazy<AppInfo> Info; internal Package Package => Info.Value.Package;

    static readonly IPackageDebugSettings Settings = (IPackageDebugSettings)new PackageDebugSettings();

    static readonly IApplicationActivationManager Manager = (IApplicationActivationManager)new ApplicationActivationManager();

    internal int Launch() { Manager.ActivateApplication(Id, default, ACTIVATEOPTIONS.AO_NOERRORUI, out var @this); return @this; }

    internal IEnumerable<int> Processes
    {
        get
        {
            nint @this = new(); ApplicationUserModelId @params = new();
            while ((@this = FindWindowEx(@this)) != default) using (Process @object = new(@this)) if (GetApplicationUserModelId(@object, @params) && CompareStringOrdinal(Id, @params)) yield return @object.Id;
        }
    }

    public partial bool Unpackaged => Package.IsDevelopmentMode;

    public partial bool Installed => GetPackagesByPackageFamily(Name);

    public partial bool Debug { set { var @this = Package.Id.FullName; if (value) Settings.EnableDebugging(@this); else Settings.DisableDebugging(@this); } }

    public partial bool Running { get { foreach (var _ in Processes) return true; return false; } }

    public partial void Terminate() => Settings.TerminateAllProcesses(Package.Id.FullName);
}