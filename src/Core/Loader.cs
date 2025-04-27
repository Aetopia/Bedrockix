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
    internal Loader(Game @this) => Game = @this;

    readonly Game Game;

    static readonly nint Address = GetProcAddress(GetModuleHandle("Kernel32"));

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
            nint @params = new(), @object = new();
            var nSize = sizeof(char) * (item.Path.Length + 1);

            try
            {
                WriteProcessMemory(@this.Handle, @params = VirtualAllocEx(@this.Handle, dwSize: nSize), item.Path, nSize);
                WaitForSingleObject(@object = CreateRemoteThread(@this.Handle, lpStartAddress: Address, lpParameter: @params));
            }
            finally { VirtualFreeEx(@this.Handle, @params); CloseHandle(@object); }
        }

        return @this.Id;
    }
}