using System;
using System.IO;
using System.Threading;
using Windows.Management.Core;
using Bedrockix.Windows;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

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

        var value = App.Launch(); nint handle = default; var @object = true;
        try
        {
            handle = OpenProcess(SYNCHRONIZE, false, value);
            ThreadPool.QueueUserWorkItem((_) => { WaitForSingleObject(handle, Timeout.Infinite); @object = default; @event.Set(); });
            @event.Wait();
        }
        finally { CloseHandle(handle); }

        return @object ? value : throw new OperationCanceledException();
    }

    public static void Terminate() => App.Terminate();
}