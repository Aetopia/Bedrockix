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

    public static partial int? Launch(params IEnumerable<string> value) => Launch([.. value.Select(_ => new Library(_))]);

    public static partial int? Launch(params IReadOnlyCollection<Library> value)
    {
        foreach (var item in value)
        {
            FileInfo info = new(item.Path);

            if (!item.Exists || !info.Exists) throw new FileNotFoundException(default, item.Path);
            if (!item.Valid) throw new BadImageFormatException(default, item.Path);

            var security = info.GetAccessControl();
            security.SetAccessRule(Rule);
            info.SetAccessControl(security);
        }

        using var instance = Game.Launch();
        if (!instance.Running) return null;

        foreach (var item in value)
        {
            nint address = default, handle = default;
            var size = Marshal.SystemDefaultCharSize * (item.Path.Length + 1);

            try
            {
                WriteProcessMemory(instance.Handle, address = VirtualAllocEx(instance.Handle, default, size, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE), item.Path, size, default);
                WaitForSingleObject(handle = CreateRemoteThread(instance.Handle, default, default, Address, address, default, default), Timeout.Infinite);
            }
            finally
            {
                VirtualFreeEx(instance.Handle, address, default, MEM_RELEASE);
                CloseHandle(handle);
            }
        }

        return instance.Id;
    }
}
