using System.IO;
using System.Threading;
using Bedrockix.Windows;
using Windows.Management.Core;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;
using System;

namespace Bedrockix.Minecraft;

/// <summary>
/// Provides methods to interact with Minecraft: Bedrock Edition.
/// </summary>

public static class Game
{
    internal static readonly App App = new("Microsoft.MinecraftUWP_8wekyb3d8bbwe!App");

    internal static Process? Activate()
    {
        var path = ApplicationDataManager.CreateForPackageFamily(App.Package.Id.FamilyName).LocalFolder.Path;

        if (!Running || File.Exists(Path.Combine(path, @"games\com.mojang\minecraftpe\resource_init_lock")))
        {
            using Event @event = new();
            using FileSystemWatcher watcher = new(path, "resource_init_lock")
            {
                NotifyFilter = NotifyFilters.FileName,
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };
            watcher.Deleted += (_, _) => @event.Set();

            var process = App.Activate();

            unsafe
            {
                var handles = stackalloc nint[] { @event, process };

                if (WaitForMultipleObjects(2, handles, default, Timeout.Infinite))
                    using (process) return default;
            }

            return process;
        }

        return App.Activate();
    }

    /// <summary>
    /// Launches Minecraft: Bedrock Edition.
    /// </summary>

    /// <returns>
    /// The process ID of the game.
    /// </returns>

    public static int? Launch() { using var @this = Activate(); return @this?.Id; }

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