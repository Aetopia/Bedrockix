using System;
using System.IO;
using System.Threading;
using Bedrockix.Windows;
using Windows.Management.Core;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Minecraft;

public static class Game
{
    static readonly App App = new("Microsoft.MinecraftUWP_8wekyb3d8bbwe!App");

    const string Value = @"games\com.mojang\minecraftpe\resource_init_lock";

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

    public static bool Running => App.Running;

    public static bool Debug { set { App.Debug = value; } }

    public static void Terminate(bool value = default) => App.Terminate(value);
}