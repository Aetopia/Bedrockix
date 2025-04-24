using System;
using System.IO;
using System.Linq;
using System.Threading;
using Bedrockix.Windows;
using System.Security.Principal;
using System.Collections.Generic;
using System.Security.AccessControl;
using static Bedrockix.Unmanaged.Native;

namespace Bedrockix.Core;

public sealed partial class Loader
{
    internal Loader(Game value) => Game = value;

    readonly Game Game;

    static readonly nint lpStartAddress = GetProcAddress(GetModuleHandle("Kernel32"));

    static readonly FileSystemAccessRule Rule = new(new SecurityIdentifier("S-1-15-2-1"), FileSystemRights.FullControl, AccessControlType.Allow);

    public partial int? Launch(params IEnumerable<string> value) => Launch([.. value.Select(_ => new Library(_))]);

    public partial int? Launch(params IReadOnlyCollection<Library> value)
    {
        foreach (var item in value)
        {
            FileInfo info = new(item.Path);

            if (!item.Exists || !info.Exists) throw new FileNotFoundException(null, item.Path);
            if (!item.Valid) throw new BadImageFormatException(null, item.Path);

            var security = info.GetAccessControl();
            security.SetAccessRule(Rule);
            info.SetAccessControl(security);
        }

        using var @this = Game.Launch();
        if (!@this[false]) return null;

        foreach (var item in value)
        {
            nint lpParameter = default, hThread = default;
            var nSize = sizeof(char) * (item.Path.Length + 1);

            try
            {
                WriteProcessMemory(@this.Handle, lpParameter = VirtualAllocEx(@this.Handle, dwSize: nSize), item.Path, nSize);
                _ = WaitForSingleObject(hThread = CreateRemoteThread(@this.Handle, lpStartAddress: lpStartAddress, lpParameter: lpParameter), Timeout.Infinite);
            }
            finally { VirtualFreeEx(@this.Handle, lpParameter); CloseHandle(hThread); }
        }

        return @this.Id;
    }
}