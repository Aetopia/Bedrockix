using System.Threading;
using Bedrockix.Windows;
using Bedrockix.Unmanaged;
using Windows.Management.Core;

namespace Bedrockix.Minecraft;

public static partial class Game
{
    internal static readonly App App = new("Microsoft.MinecraftUWP_8wekyb3d8bbwe");

    internal unsafe static Handle.Process Launch()
    {
        fixed (char* path = @$"{ApplicationDataManager.CreateForPackageFamily(App.Package.Id.FamilyName).LocalFolder.Path}\games\com.mojang\minecraftpe\menu_load_lock")
            if (!App.Running || Wrappers.Exists(path) || Manifest.Current.Instancing)
            {
                Handle.Process process = new(App.Launch());
                SpinWait wait = new(); var value = false;

                while (process.Running)
                {
                    if (value) { if (!Wrappers.Exists(path)) break; }
                    else value = Wrappers.Exists(path);
                    wait.SpinOnce();
                }

                return process;
            }

        return new(App.Launch());
    }

    public static partial int? Launch(bool value)
    {
        if (!value) return App.Launch();

        using var instance = Launch();
        return instance.Running ? instance.Id : null;
    }

    public static partial void Terminate() => App.Terminate();

    public static partial bool Installed => App.Installed;

    public static partial bool Running => App.Running;

    public static partial bool Debug { set { App.Debug = value; } }

    public static partial bool Unpackaged => App.Package.IsDevelopmentMode;
}