using Bedrockix.Unmanaged;
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
        unsafe
        {
            fixed (char* path = Path = System.IO.Path.GetFullPath(value))
                if (Exists = Wrappers.Exists(path) && System.IO.Path.HasExtension(Path))
                    Valid = !GetBinaryType(Path, out _) && FreeLibrary(LoadLibraryEx(Path, default, DONT_RESOLVE_DLL_REFERENCES));
        }
    }
}