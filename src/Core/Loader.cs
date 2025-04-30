using System;
using System.IO;
using System.Linq;
using Bedrockix.Windows;
using System.Security.Principal;
using System.Collections.Generic;
using System.Security.AccessControl;
using static Bedrockix.Unmanaged.Safe;
using Bedrockix.Unmanaged;

namespace Bedrockix.Core;

public sealed partial class Loader
{
    internal Loader(Game @this) => Game = @this;

    readonly Game Game;

    static readonly nint Address = GetProcAddress();

    static readonly FileSystemAccessRule Rule = new(new SecurityIdentifier("S-1-15-2-1"), FileSystemRights.FullControl, AccessControlType.Allow);

    public partial int? Launch(params IEnumerable<string> value) => Launch([.. value.Select(_ => new Library(_))]);

    public partial int? Launch(params IReadOnlyCollection<Library> value)
    {
        foreach (var item in value)
        {
            FileInfo info = new(item.Path);

            if (!info.Exists || string.IsNullOrEmpty(info.Extension)) throw new FileNotFoundException(default, info.FullName);
            else if (!LoadLibraryEx(info.FullName)) throw new BadImageFormatException(default, info.FullName);

            var security = info.GetAccessControl(); security.SetAccessRule(Rule); info.SetAccessControl(security);
        }

        using var @this = Game.Launch(); if (!@this[false]) return null;

        foreach (var item in value)
        {
            using var @params = VirtualAllocEx(@this, sizeof(char) * (item.Path.Length + 1)); @params.Write(item.Path);
            using Thread @object = new(@this, Address, @params); @object.Wait();
        }

        return @this.Id;
    }
}