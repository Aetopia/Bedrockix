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
        var path = @$"{ApplicationDataManager.CreateForPackageFamily(App.Package.Id.FamilyName).LocalFolder.Path}\games\com.mojang\minecraftpe\resource_init_lock";

        if (!App.Running || File.Exists(path))
        {
            SpinWait _ = new();
            Process @this = new(App.Launch());
            nint handle = INVALID_HANDLE_VALUE;

            try
            {
                do { _.SpinOnce(); if (!@this.Running) return @this; }
                while ((handle = CreateFile2(path, default, FILE_SHARE_DELETE, OPEN_EXISTING, default)) is INVALID_HANDLE_VALUE);

                _.Reset();

                do { _.SpinOnce(); if (!@this.Running) return @this; }
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
        using var @this = Launch();
        return @this.Running ? @this.Id : null;
    }

    public static partial void Terminate() => App.Terminate();

    public static partial bool Installed => App.Installed;

    public static partial bool Running => App.Running;

    public static partial bool Debug { set => App.Debug = value; }

    public static partial bool Unpackaged => App.Package.IsDevelopmentMode;
}