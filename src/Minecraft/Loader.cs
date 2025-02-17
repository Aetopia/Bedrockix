using System.IO;
using Bedrockix.Windows;
using System.ComponentModel;
using System.Security.Principal;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Runtime.InteropServices;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Minecraft;

/// <summary>
/// Provides methods to load dynamic link libraries into Minecraft: Bedrock Edition.
/// </summary>
public static class Loader
{
    static readonly nint Address;

    static Loader()
    {
        using Library library = new("Kernel32.dll");
        Address = library["LoadLibraryW"];
    }

    static readonly SecurityIdentifier Identifier = new("S-1-15-2-1");

    static string Get(string path)
    {
        FileInfo info = new(path);

        if (!info.Exists)
            throw new FileNotFoundException();
        var security = info.GetAccessControl();
        security.AddAccessRule(new(Identifier, FileSystemRights.ReadAndExecute, AccessControlType.Allow));

        info.SetAccessControl(security);

        return info.FullName;
    }

    static void Load(nint value, string path)
    {
        var size = sizeof(char) * ((path = Get(path)).Length + 1);
        var buffer = Marshal.StringToHGlobalUni(path);

        try
        {
            using Region region = new(value, size);

            if (!WriteProcessMemory(value, region, buffer, size, default))
                throw new Win32Exception(Marshal.GetLastWin32Error());

            var @object = CreateRemoteThread(value, default, default, Address, region, default, default);
            if (@object == default) throw new Win32Exception(Marshal.GetLastWin32Error());

            using Handle handle = new(@object);
            Handle.Single(handle);
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    /// <summary>
    /// Launches &amp; loads a dynamic link library into Minecraft: Bedrock Edition.
    /// </summary>
    /// <param name="path">The dynamic link library to load.</param>
    public static void Launch(string path)
    {
        using Handle handle = new(OpenProcess(PROCESS_ALL_ACCESS, false, Game.Launch()));
        Load(handle, path);
    }

    /// <summary>
    /// Launches &amp; loads dynamic link libraries into Minecraft: Bedrock Edition.
    /// </summary>
    /// <param name="paths">The dynamic link libraries to load.</param>
    public static void Launch(params IEnumerable<string> paths)
    {
        using Handle handle = new(OpenProcess(PROCESS_ALL_ACCESS, false, Game.Launch()));
        foreach (var path in paths) Load(handle, path);
    }
}