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

    const string Value = @"games\com.mojang\minecraftpe\resource_init_lock";

    /// <summary>
    /// Launch an instance of Minecraft: Bedrock Edition.
    /// </summary>
    /// <returns>The process ID of the instance.</returns>
    /// <exception cref="OperationCanceledException">Thrown if the instance terminates prematurely.</exception>
    public static int Launch()
    {
        var path = ApplicationDataManager.CreateForPackageFamily(App.Package.Id.FamilyName).LocalFolder.Path;

        if (!(Running && !File.Exists(Path.Combine(path, Value))))
        {
            using ManualResetEventSlim @event = new();

            using FileSystemWatcher watcher = new(path)
            {
                NotifyFilter = NotifyFilters.FileName,
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };

            watcher.Deleted += (_, e) =>
            {
                if (e.Name.Equals(Value, StringComparison.OrdinalIgnoreCase))
                    @event.Set();
            };

            var value = App.Launch();
            nint handle = default;
            bool @object = default;

            try
            {
                handle = OpenProcess(SYNCHRONIZE, false, value);
                ThreadPool.UnsafeQueueUserWorkItem((_) =>
                {
                    WaitForSingleObject(handle, Timeout.Infinite);
                    @object = true;
                    @event.Set();
                }, default);
                @event.Wait();
            }
            finally { CloseHandle(handle); }

            return @object ? throw new OperationCanceledException() : value;
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