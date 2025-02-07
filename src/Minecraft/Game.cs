using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Windows.Management.Core;
using Bedrockix.Windows;

namespace Bedrockix.Minecraft;

/// <summary>
/// Provides methods to interact with Minecraft: Bedrock Edition.
/// </summary>
public static class Game
{
    static readonly App App = new("Microsoft.MinecraftUWP_8wekyb3d8bbwe!App") { Lifecycle = default };

    const string Value = @"games\com.mojang\minecraftpe\resource_init_lock";

    /// <summary>
    /// Launches the game and waits for it to fully initialize.
    /// </summary>
    /// <returns>The PID of the game.</returns>
    /// <exception cref="OperationCanceledException">Thrown if the game terminates prematurely.</exception>
    public static int Launch()
    {
        var path = ApplicationDataManager.CreateForPackageFamily(App.Package.Id.FamilyName).LocalFolder.Path;
        using ManualResetEventSlim @event = new(App.Running && !File.Exists(Path.Combine(path, Value)));

        using FileSystemWatcher watcher = new(path) { NotifyFilter = NotifyFilters.FileName, IncludeSubdirectories = true, EnableRaisingEvents = true };
        watcher.Deleted += (_, e) => { if (e.Name.Equals(Value, StringComparison.OrdinalIgnoreCase)) @event.Set(); };

        using var process = Process.GetProcessById(App.Launch()); process.EnableRaisingEvents = true;
        var _ = false; process.Exited += (_, _) => { _ = true; @event.Set(); };
        @event.Wait(); return _ ? throw new OperationCanceledException() : process.Id;
    }

    /// <summary>
    /// Terminates the game.
    /// </summary>
    public static void Terminate() => App.Terminate();
}