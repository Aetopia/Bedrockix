using System;
using System.IO;
using System.Threading;
using Bedrockix.Minecraft.Windows;
using Windows.Management.Core;
using static Bedrockix.Minecraft.Unmanaged.Native;
using static Bedrockix.Minecraft.Unmanaged.Constants;

namespace Bedrockix.Minecraft;

/// <summary>
/// Provides methods to interact with Minecraft: Bedrock Edition.
/// </summary>
public static class Game
{
    static readonly App App = new("Microsoft.MinecraftUWP_8wekyb3d8bbwe!App");

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
            using ManualResetEventSlim @event = new();

            FileSystemWatcher watcher = new(path, "resource_init_lock")
            {
                NotifyFilter = NotifyFilters.FileName,
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };
            watcher.Deleted += (_, _) => @event.Set();

            var value = App.Launch();

            using Handle handle = new(OpenProcess(SYNCHRONIZE, false, value));
            WaitHandle.WaitAny([@event.WaitHandle, handle]);

            return @event.IsSet ? value : throw new OperationCanceledException();
        }

        return App.Launch();
    }

    /// <summary>
    /// Check for any running instance of Minecraft: Bedrock Edition.
    /// </summary>
    public static bool Running => App.Running;


    /// <summary>
    /// Configure debug mode for Minecraft: Bedrock Edition.
    /// </summary>
    public static bool Debug { set { App.Debug = value; } }


    /// <summary>
    /// Terminate any running instances of Minecraft: Bedrock Edition.
    /// </summary>
    public static void Terminate() => App.Terminate();
}