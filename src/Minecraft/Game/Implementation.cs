using System.IO;
using System.Threading;
using Bedrockix.Windows;
using Windows.Management.Core;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Minecraft;

public static partial class Game
{
    internal static readonly App App = new("Microsoft.MinecraftUWP_8wekyb3d8bbwe!App");

    internal static Process? Activate()
    {
        var path = ApplicationDataManager.CreateForPackageFamily(App.Package.Id.FamilyName).LocalFolder.Path;
        var value = Path.Combine(path, @"games\com.mojang\minecraftpe\resource_init_lock");
        var flag = File.Exists(value);

        if (!Running || flag)
        {
            var process = App.Activate();

            SpinWait.SpinUntil(() =>
            {
                if (!process.Running) using (process) return true;
                else if (flag && !File.Exists(value)) return true;
                flag = File.Exists(value); return false;
            });

            return process.Running ? process : null;
        }

        return App.Activate();
    }

    public static partial int? Launch() { using var @this = Activate(); return @this?.Id; }

    public static partial void Terminate() => App.Terminate();

    public static partial bool Installed => GetPackagesByPackageFamily("Microsoft.MinecraftUWP_8wekyb3d8bbwe", out _, default, out _, default) is ERROR_INSUFFICIENT_BUFFER;

    public static partial bool Running => App.Running;

    public static partial bool Debug { set { App.Debug = value; } }
}