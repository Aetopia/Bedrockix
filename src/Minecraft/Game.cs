using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Windows.Management.Core;
using Bedrockix.Windows;

namespace Bedrockix.Minecraft;

public static class Game
{
    static readonly App App = new("Microsoft.MinecraftUWP_8wekyb3d8bbwe!App") { Debug = true };

    const string Value = @"games\com.mojang\minecraftpe\resource_init_lock";

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

    public static void Terminate() => App.Terminate();
}