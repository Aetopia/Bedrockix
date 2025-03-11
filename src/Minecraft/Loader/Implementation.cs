using System;
using System.IO;
using System.Linq;
using System.Threading;
using Bedrockix.Windows;
using System.Security.Principal;
using System.Collections.Generic;
using System.Security.AccessControl;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Minecraft;

public static partial class Loader
{
    static readonly nint Address = GetProcAddress(GetModuleHandle("Kernel32"), "LoadLibraryW");

    static readonly FileSystemAccessRule Rule = new(new SecurityIdentifier("S-1-15-2-1"), FileSystemRights.FullControl, AccessControlType.Allow);

    static string Get(Library library)
    {
        if (!library.Exists) throw new FileNotFoundException(default, library.Path);
        if (!library.Valid) throw new BadImageFormatException(default, library.Path);

        FileInfo info = new(library.Path);
        var security = info.GetAccessControl();
        security.SetAccessRule(Rule);
        info.SetAccessControl(security);

        return library.Path;
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

    static int? Activate(string path)
    {
        using var @this = Game.Activate();
        if (@this is Process process) { Load(process, path); return process.Id; }
        return null;
    }

    static int? Activate(IEnumerable<string> paths)
    {
        using var @this = Game.Activate();
        if (@this is Process process) { foreach (var path in paths) Load(process, path); return process.Id; }
        return null;
    }

    public static partial int? Launch(params IEnumerable<string> paths) => Launch(paths.Select(_ => new Library(_)));

    public static partial int? Launch(params IEnumerable<Library> libraries) => Activate([.. libraries.Select(Get)]);
}
