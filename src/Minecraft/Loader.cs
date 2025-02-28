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
    static readonly nint Address = GetProcAddress(GetModuleHandle("Kernel32"), "LoadLibraryW");

    static readonly FileSystemAccessRule Rule = new(new SecurityIdentifier("S-1-15-2-1"), FileSystemRights.FullControl, AccessControlType.Allow);

    static void Load(nint value, string path)
    {
        FileInfo info = new(path);
        if (!info.Exists) return;

        var security = info.GetAccessControl();
        security.SetAccessRule(Rule);
        info.SetAccessControl(security);

        var size = sizeof(char) * (info.FullName.Length + 1);
        nint address = default;

        try
        {
            if ((address = VirtualAllocEx(value, default, size, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE)) == default)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            if (!WriteProcessMemory(value, address, info.FullName, size, default))
                throw new Win32Exception(Marshal.GetLastWin32Error());

            var @object = CreateRemoteThread(value, default, default, Address, address, default, default);
            if (@object== default) throw new Win32Exception(Marshal.GetLastWin32Error());

            using Handle handle = new(@object); Handle.Wait(handle);
        }
        finally { VirtualFreeEx(value, address, default, MEM_RELEASE); }
    }

    /// <summary>
    /// Launches &amp; loads a dynamic link library into Minecraft: Bedrock Edition.
    /// </summary>

    /// <param name="path">
    /// The dynamic link library to load.
    /// </param>

    /// <returns>
    /// The process ID of the game.
    /// </returns>

    public static int? Launch(string path)
    {
        var value = Game.Launch(out var handle);
        using (handle) if (value.HasValue) Load(handle, path);
        return value;
    }

    /// <summary>
    /// Launches &amp; loads dynamic link libraries into Minecraft: Bedrock Edition.
    /// </summary>

    /// <param name="paths">
    /// The dynamic link libraries to load.
    /// </param>

    /// <returns>
    /// The process ID of the game.
    /// </returns>

    public static int? Launch(params IEnumerable<string> paths)
    {
        var value = Game.Launch(out var handle);
        using (handle) if (value.HasValue) foreach (var path in paths) Load(handle, path);
        return value;
    }
}