using System.IO;
using Bedrockix.Windows;
using System.Security.Principal;
using System.Collections.Generic;
using System.Security.AccessControl;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;
using System.Linq;
using System.Threading;

namespace Bedrockix.Minecraft;

/// <summary>
/// Provides methods to load dynamic link libraries into Minecraft: Bedrock Edition.
/// </summary>

public static class Loader
{
    static readonly nint Address = GetProcAddress(GetModuleHandle("Kernel32"), "LoadLibraryW");

    static readonly FileSystemAccessRule Rule = new(new SecurityIdentifier("S-1-15-2-1"), FileSystemRights.FullControl, AccessControlType.Allow);

    static string Validate(string path)
    {
        FileInfo info = new(path);
        if (!info.Exists) throw new FileNotFoundException(default, info.FullName);

        var security = info.GetAccessControl();

        security.SetAccessRule(Rule);
        info.SetAccessControl(security);

        return info.FullName;
    }

    static void Load(Process process, string path)
    {
        nint address = default, handle = default;
        var size = sizeof(char) * (path.Length + 1);

        try
        {
            WriteProcessMemory(process, address = VirtualAllocEx(process, default, size, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE), path, size, default);
            WaitForSingleObject(handle = CreateRemoteThread(process, default, default, Address, address, default, default), Timeout.Infinite);
        }
        finally
        {
            VirtualFreeEx(process, address, default, MEM_RELEASE);
            CloseHandle(handle);
        }
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
        path = Validate(path); using var @this = Game.Activate();

        if (@this is Process process)
        {
            Load(process, path);
            return process.Id;
        }

        return default;
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
        paths = [.. paths.Select(Validate)]; using var @this = Game.Activate();

        if (@this is Process process)
        {
            foreach (var path in paths) Load(process, path);
            return process.Id;
        }

        return default;
    }
}