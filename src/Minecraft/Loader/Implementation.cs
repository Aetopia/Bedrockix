using System;
using System.IO;
using System.Linq;
using System.Threading;
using Bedrockix.Windows;
using System.Security.Principal;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Runtime.InteropServices;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Minecraft;

public static partial class Loader
{
    static readonly nint Address = GetProcAddress(GetModuleHandle("Kernel32"), "LoadLibraryW");

    static readonly FileSystemAccessRule Rule = new(new SecurityIdentifier("S-1-15-2-1"), FileSystemRights.FullControl, AccessControlType.Allow);

    static void Validate(IReadOnlyCollection<Library> libraries)
    {
        foreach (var library in libraries)
        {
            FileInfo info = new(library.Path);

            if (!library.Exists || !info.Exists) throw new FileNotFoundException(default, library.Path);
            if (!library.Valid) throw new BadImageFormatException(default, library.Path);

            var security = info.GetAccessControl();
            security.SetAccessRule(Rule);
            info.SetAccessControl(security);
        }
    }

    static void Load(nint process, IReadOnlyCollection<Library> libraries)
    {
        foreach (var library in libraries)
        {
            nint parameter = default, thread = default;
            var size = Marshal.SystemDefaultCharSize * (library.Path.Length + 1);

            try
            {
                WriteProcessMemory(process, parameter = VirtualAllocEx(process, default, size, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE), library.Path, size, default);
                WaitForSingleObject(thread = CreateRemoteThread(process, default, default, Address, parameter, default, default), Timeout.Infinite);
            }
            finally
            {
                VirtualFreeEx(process, parameter, default, MEM_RELEASE);
                CloseHandle(thread);
            }
        }
    }

    public static partial int? Launch(params IEnumerable<string> paths) => Launch([.. paths.Select(_ => new Library(_))]);

    public static partial int? Launch(params IReadOnlyCollection<Library> libraries)
    {
        Validate(libraries);

        using var process = Game.Launch();
        if (!process.Running) return null;

        Load(process.Handle, libraries);
        return process.Id;
    }
}
