using System.IO;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

public sealed partial class Library
{
    public partial Library(string path)
    {
        if (Exists = File.Exists(Path = System.IO.Path.GetFullPath(path)) && System.IO.Path.HasExtension(Path))
            Valid = !GetBinaryType(Path, out _) && FreeLibrary(LoadLibraryEx(Path, default, DONT_RESOLVE_DLL_REFERENCES));
    }
}