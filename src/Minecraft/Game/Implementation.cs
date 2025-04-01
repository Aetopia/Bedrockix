using System.Threading;
using Bedrockix.Windows;
using Bedrockix.Unmanaged;
using Windows.Management.Core;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Minecraft;

public static partial class Game
{
    internal static readonly App App = new("Microsoft.MinecraftUWP_8wekyb3d8bbwe!App");

    internal unsafe static Process Launch()
    {
        fixed (char* path = @$"{ApplicationDataManager.CreateForPackageFamily(App.Package.Id.FamilyName).LocalFolder.Path}\games\com.mojang\minecraftpe\resource_init_lock")
            if (!App.Running || Wrappers.Exists(path) || Manifest.Current.Instancing)
            {
                Process process = new(App.Launch());
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

        using var process = Launch();
        return process.Running ? process.Id : null;
    }

    public static partial void Terminate() => App.Terminate();

    public static partial bool Installed => GetPackagesByPackageFamily("Microsoft.MinecraftUWP_8wekyb3d8bbwe", out _, default, out _, default) is ERROR_INSUFFICIENT_BUFFER;

    public static partial bool Running => App.Running;

    public static partial bool Debug { set { App.Debug = value; } }

    public static partial bool Unpackaged => App.Package.IsDevelopmentMode;
}