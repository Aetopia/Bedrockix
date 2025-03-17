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

    internal unsafe static Process Activate()
    {
        fixed (char* path = Path.Combine(ApplicationDataManager.CreateForPackageFamily(App.Package.Id.FamilyName).LocalFolder.Path, @"games\com.mojang\minecraftpe\resource_init_lock"))
        {
            var value = Get(path);
            if (!Running || value) return Activate(path, value);
            return App.Activate();
        }
    }

    unsafe static Process Activate(char* path, bool value)
    {
        var process = App.Activate();

        SpinWait.SpinUntil(() =>
        {
            if (!process.Running) using (process) return true;
            else if (value && !Get(path)) return true;
            else if (!value) value = Get(path);
            return false;
        });

        return process.Running ? process : null;
    }

    unsafe static bool Get(char* path)
    {
        var value = GetFileAttributes(path);
        if (value is INVALID_FILE_ATTRIBUTES) return false;
        return (value & FILE_ATTRIBUTE_DIRECTORY) == default;
    }

    public static partial int? Launch(bool value)
    {
        using var process = value ? Activate() : App.Activate();
        return process?.Id;
    }

    public static partial void Terminate() => App.Terminate();

    public static partial bool Installed => GetPackagesByPackageFamily("Microsoft.MinecraftUWP_8wekyb3d8bbwe", out _, default, out _, default) is ERROR_INSUFFICIENT_BUFFER;

    public static partial bool Running => App.Running;

    public static partial bool Debug { set { App.Debug = value; } }
}