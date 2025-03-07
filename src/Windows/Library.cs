using System;
using System.IO;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;
using System.Collections.Generic;
using System.Linq;

namespace Bedrockix.Windows;

public sealed class Library
{
    public bool Exists { get; private set; }

    public bool Valid { get; private set; }

    public readonly string Path;

    Library(string path)
    {
        Path = System.IO.Path.GetFullPath(path);
        if (Exists = File.Exists(Path) && System.IO.Path.GetExtension(Path).Equals(".dll", StringComparison.OrdinalIgnoreCase))
            Valid = !GetBinaryType(Path, out _) && FreeLibrary(LoadLibraryEx(Path, default, DONT_RESOLVE_DLL_REFERENCES));
    }

    public static Library Get(string path) => new(path);

    public static Library[] Get(params IEnumerable<string> paths) => [.. paths.Select(_ => new Library(_))];
}