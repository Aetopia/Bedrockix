using System.IO;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

public sealed partial class Library
{
    /// <summary>
    /// Resolves a dynamic link library.
    /// </summary>

    /// <param name="value">
    /// The dynamic link library to resolve.
    /// </param>

    public Library(string value)
    {
        if (Exists = File.Exists(Path = System.IO.Path.GetFullPath(value)) && System.IO.Path.HasExtension(Path))
            Valid = FreeLibrary(LoadLibraryEx(Path));
    }
}