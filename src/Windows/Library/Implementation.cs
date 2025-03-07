using System.IO;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

public sealed partial class Library
{
    /// <summary>
    /// Resolves a dynamic link library.
    /// </summary>
    
    /// <param name="path">
    /// The dynamic link library to resolve.
    /// </param>

    public Library(string path)
    {
        Path = System.IO.Path.GetFullPath(path);
        if (Exists = File.Exists(Path) && System.IO.Path.HasExtension(Path))
            Valid = !GetBinaryType(Path, out _) && FreeLibrary(LoadLibraryEx(Path, default, DONT_RESOLVE_DLL_REFERENCES));
    }
}