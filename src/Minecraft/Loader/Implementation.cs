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

namespace Bedrockix.Minecraft;

public static partial class Loader
{
    static readonly nint LoadLibraryW = GetProcAddress(GetModuleHandle("Kernel32"), "LoadLibraryW");

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

        using var @this = Game.Launch();
        if (!@this[default]) return null;

        foreach (var item in value)
        {
            nint lpParameter = default, hThread = default;
            var nSize = Marshal.SystemDefaultCharSize * (item.Path.Length + 1);

            try
            {
                WriteProcessMemory(@this.Handle, lpParameter = VirtualAllocEx(@this.Handle, default, nSize), item.Path, nSize);
                _ = WaitForSingleObject(hThread = CreateRemoteThread(@this.Handle, lpStartAddress: LoadLibraryW, lpParameter: lpParameter), Timeout.Infinite);
            }
            finally
            {
                VirtualFreeEx(@this.Handle, lpParameter);
                CloseHandle(hThread);
            }
        }

        return @this.Id;
    }
}
