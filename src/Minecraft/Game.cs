using System.IO;
using Bedrockix.Windows;
using Windows.Management.Core;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Minecraft;

/// <summary>
/// Provides methods to interact with Minecraft: Bedrock Edition.
/// </summary>

public static class Game
{
    internal static readonly App App = new("Microsoft.MinecraftUWP_8wekyb3d8bbwe!App");

    internal static int? Launch(out Handle handle)
    {
        int value; var path = ApplicationDataManager.CreateForPackageFamily(App.Package.Id.FamilyName).LocalFolder.Path;

        if (!Running || File.Exists(Path.Combine(path, @"games\com.mojang\minecraftpe\resource_init_lock")))
        {
            var flag = false; using Handle @event = new(CreateEvent(default, default, default, default));

            using FileSystemWatcher watcher = new(path, "resource_init_lock") { NotifyFilter = NotifyFilters.FileName, IncludeSubdirectories = true, EnableRaisingEvents = true };
            watcher.Deleted += (_, _) => { flag = true; SetEvent(@event); };

            unsafe
            {
                var handles = stackalloc nint[] { handle = new(OpenProcess(PROCESS_ALL_ACCESS, false, value = App.Launch())), @event };
                Handle.Wait(2, handles);
            }

            return flag ? value : null;
        }

        handle = new(OpenProcess(PROCESS_ALL_ACCESS, false, value = App.Launch()));
        return value;
    }

    /// <summary>
    /// Launches Minecraft: Bedrock Edition.
    /// </summary>

    /// <returns>
    /// The process ID of the game.
    /// </returns>

    public static int? Launch()
    {
        var value = Launch(out var handle);
        using (handle) return value;
    }

    /// <summary>
    /// Terminates Minecraft: Bedrock Edition.
    /// </summary>

    public static void Terminate() => App.Terminate();

    /// <summary>
    /// Check if Minecraft: Bedrock Edition is installed.
    /// </summary>

    public static bool Installed => GetPackagesByPackageFamily("Microsoft.MinecraftUWP_8wekyb3d8bbwe", out _, default, out _, default) is ERROR_INSUFFICIENT_BUFFER;

    /// <summary>
    /// Check if Minecraft: Bedrock Edition is running.
    /// </summary>

    public static bool Running => App.Running;

    /// <summary>
    /// Configure debug mode for Minecraft: Bedrock Edition.
    /// </summary>

    public static bool Debug { set { App.Debug = value; } }
}