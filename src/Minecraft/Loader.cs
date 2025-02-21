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

    static readonly FileSystemAccessRule Rule = new(new SecurityIdentifier("S-1-15-2-1"), FileSystemRights.FullControl, AccessControlType.Allow);

    static Loader()
    {
        using Library library = new("Kernel32.dll");
        Address = library["LoadLibraryW"];
    }

    static string Get(string path)
    {
        FileInfo info = new(path);
        if (!info.Exists) throw new FileNotFoundException();

        var security = info.GetAccessControl();
        security.SetAccessRule(Rule);

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
    /// /// <returns>The process ID of the loaded instance.</returns>
    public static int Launch(string path)
    {
        var value = Game.Launch();
        using Handle handle = new(OpenProcess(PROCESS_ALL_ACCESS, false, value));
        Load(handle, path); return value;
    }

    /// <summary>
    /// Launches &amp; loads dynamic link libraries into Minecraft: Bedrock Edition.
    /// </summary>
    /// <param name="paths">The dynamic link libraries to load.</param>
    /// <returns>The process ID of the loaded instance.</returns>
    public static int Launch(params IEnumerable<string> paths)
    {
        var value = Game.Launch();
        using Handle handle = new(OpenProcess(PROCESS_ALL_ACCESS, false, value));
        foreach (var path in paths) Load(handle, path); return value;
    }
}