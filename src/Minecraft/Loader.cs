using System.IO;
using Bedrockix.Windows;
using System.Security.Principal;
using System.Collections.Generic;
using System.Security.AccessControl;
using static Bedrockix.Unmanaged.Native;

namespace Bedrockix.Minecraft;

/// <summary>
/// Provides methods to load dynamic link libraries into Minecraft: Bedrock Edition.
/// </summary>

public static class Loader
{
    static readonly nint Address = GetProcAddress(GetModuleHandle("Kernel32"), "LoadLibraryW");

    static readonly FileSystemAccessRule Rule = new(new SecurityIdentifier("S-1-15-2-1"), FileSystemRights.FullControl, AccessControlType.Allow);

    static void Load(nint handle, string value)
    {
        FileInfo info = new(value);
        if (!info.Exists) return;

        var security = info.GetAccessControl();
        security.SetAccessRule(Rule);
        info.SetAccessControl(security);

        using Region region = new(handle, value);
        using Handle _ = new(CreateRemoteThread(handle, default, default, Address, region, default, default));
        Handle.Wait(_);
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