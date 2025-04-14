using System.IO;
using System.Threading;
using Bedrockix.Windows;
using Windows.Management.Core;
using Bedrockix.Unmanaged.Types;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Minecraft;

public static partial class Game
{
    internal static readonly App App = new("Microsoft.MinecraftUWP_8wekyb3d8bbwe");

    internal unsafe static Process Launch()
    {
        var path = @$"{ApplicationDataManager.CreateForPackageFamily(App.Package.Id.FamilyName).LocalFolder.Path}\games\com.mojang\minecraftpe\menu_load_lock";

        if (!App.Running || File.Exists(path) || Manifest.Current.Instancing)
        {
            SpinWait _ = new();
            nint handle = INVALID_HANDLE_VALUE;
            Process @this = new(App.Launch());

            try
            {
                do { _.SpinOnce(); if (!@this.Running) break; }
                while ((handle = CreateFile2(path, default, FILE_SHARE_DELETE, OPEN_EXISTING, default)) is INVALID_HANDLE_VALUE);

                _.Reset();

                do { _.SpinOnce(); if (!@this.Running) break; }
                while (GetFileInformationByHandleEx(handle, FileStandardInfo, out var value, sizeof(FILE_STANDARD_INFO)) && !value.DeletePending);

                return @this;
            }
            finally { CloseHandle(handle); }
        }

        return new(App.Launch());
    }

    public static partial int? Launch(bool value)
    {
        if (!value) return App.Launch();

        using var process = Launch();
        return process.Running ? process.Id : null;
    }

    public static partial void Terminate() => App.Terminate();

    public static partial bool Installed => App.Installed;

    public static partial bool Running => App.Running;

    public static partial bool Debug { set { App.Debug = value; } }

    public static partial bool Unpackaged => App.Package.IsDevelopmentMode;
}