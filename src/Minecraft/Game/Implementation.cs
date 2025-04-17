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
        var handle = INVALID_HANDLE_VALUE;

        fixed (char* path = @$"{ApplicationDataManager.CreateForPackageFamily(App.Package.Id.FamilyName).LocalFolder.Path}\games\com.mojang\minecraftpe\resource_init_lock")
        {
            try
            {
                if (!App.Running || (handle = CreateFile2(path)) != INVALID_HANDLE_VALUE)
                {
                    SpinWait wait = new();
                    Process process = new(App.Launch());

                    while (handle is INVALID_HANDLE_VALUE)
                        if (process.Running)
                        {
                            handle = CreateFile2(path);
                            wait.SpinOnce();
                        }
                        else return process;

                    do
                        if (process.Running) wait.SpinOnce();
                        else return process;
                    while (GetFileInformationByHandleEx(handle, FileStandardInfo, out var value, sizeof(FILE_STANDARD_INFO)) && !value.DeletePending);

                    return process;
                }
            }
            finally { CloseHandle(handle); }
        }

        return new(App.Launch());
    }

    public static partial int? Launch(bool value)
    {
        if (!value)
            return App.Launch();

        using var process = Launch();
        return process.Running ? process.Id : null;
    }

    public static partial void Terminate() => App.Terminate();

    public static partial bool Installed => App.Installed;

    public static partial bool Running => App.Running;

    public static partial bool Debug { set => App.Debug = value; }

    public static partial bool Unpackaged => App.Package.IsDevelopmentMode;
}