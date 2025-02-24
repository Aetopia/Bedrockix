using System;
using System.IO;
using System.Threading;
using Windows.Management.Core;
using Bedrockix.Windows;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Minecraft;

/// <summary>
/// Provides methods to interact with Minecraft: Bedrock Edition.
/// </summary>
public static class Game
{
    static readonly Lazy<App> Object = new(() => new("Microsoft.MinecraftUWP_8wekyb3d8bbwe!App"), LazyThreadSafetyMode.PublicationOnly);

    internal static App App => Object.Value;

    /// <summary>
    /// Launch an instance of Minecraft: Bedrock Edition.
    /// </summary>
    /// <returns>The process ID of the instance.</returns>
    /// <exception cref="OperationCanceledException">Thrown if the instance terminates prematurely.</exception>
    public static int Launch()
    {
        var path = ApplicationDataManager.CreateForPackageFamily(App.Package.Id.FamilyName).LocalFolder.Path;

        if (!Running || File.Exists(Path.Combine(path, @"games\com.mojang\minecraftpe\resource_init_lock")))
        {
            bool flag = default; using Handle @event = new(CreateEvent(default, default, default, default));

            using FileSystemWatcher watcher = new(path, "resource_init_lock")
            {
                NotifyFilter = NotifyFilters.FileName,
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };
            watcher.Deleted += (_, _) => flag = SetEvent(@event);

            var value = App.Launch();

            using Handle @object = new(OpenProcess(SYNCHRONIZE, false, value));
            unsafe { var handles = stackalloc nint[] { @event, @object }; Handle.Any(2, handles); }

            return flag ? value : throw new OperationCanceledException();
        }

        return App.Launch();
    }

    /// <summary>
    /// Terminate any running instances of Minecraft: Bedrock Edition.
    /// </summary>
    public static void Terminate() => App.Terminate();

    /// <summary>
    /// Check if Minecraft: Bedrock Edition is installed.
    /// </summary>
    public static bool Installed => GetPackagesByPackageFamily("Microsoft.MinecraftUWP_8wekyb3d8bbwe", out _, default, out _, default) is ERROR_INSUFFICIENT_BUFFER;

    /// <summary>
    /// Check for any running instance of Minecraft: Bedrock Edition.
    /// </summary>
    public static bool Running => App.Running;

    /// <summary>
    /// Configure debug mode for Minecraft: Bedrock Edition.
    /// </summary>
    public static bool Debug { set { App.Debug = value; } }
}