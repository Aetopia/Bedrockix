using Bedrockix.Windows;
using Windows.Management.Core;
using Bedrockix.Unmanaged.Types;
using System.Runtime.InteropServices;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Minecraft;

public static partial class Game
{
    internal static readonly App App = new("Microsoft.MinecraftUWP_8wekyb3d8bbwe!App");

    public static partial int? Launch(bool value)
    {
        if (!value) return App.Launch();
        using var @this = Launch();
        return @this[default] ? @this.Id : null;
    }

    public static partial void Terminate() => App.Terminate();

    public static partial bool Installed { get { GetPackagesByPackageFamily("Microsoft.MinecraftUWP_8wekyb3d8bbwe", out var value, default, out _, default); return value; } }

    public static partial bool Running => App.Running;

    public static partial bool Debug { set => App.Debug = value; }

    public static partial bool Unpackaged => App.Package.IsDevelopmentMode;

    internal unsafe static Instance Launch()
    {
        fixed (char* lpFileName = @$"{ApplicationDataManager.CreateForPackageFamily(App.Package.Id.FamilyName).LocalFolder.Path}\games\com.mojang\minecraftpe\resource_init_lock")
        {
            var hFile = INVALID_HANDLE_VALUE;

            try
            {
                if (!Running || (hFile = CreateFile2(lpFileName)) != INVALID_HANDLE_VALUE)
                {
                    Instance @this = new(App.Launch());

                    while (hFile is INVALID_HANDLE_VALUE)
                        if (!@this[true]) return @this;
                        else hFile = CreateFile2(lpFileName);

                    while (GetFileInformationByHandleEx(hFile, FileStandardInfo, out var lpFileInformation, sizeof(FILE_STANDARD_INFO)) && !lpFileInformation.DeletePending)
                        if (!@this[true]) return @this;

                    return @this;
                }
            }
            finally { CloseHandle(hFile); }
        }

        return new(App.Launch());
    }
}