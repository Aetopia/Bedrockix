using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Unmanaged;

static partial class Wrappers
{
    internal unsafe static bool Exists(char* value)
    {
        var @this = GetFileAttributes(value);
        if (@this is INVALID_FILE_ATTRIBUTES) return false;
        return (@this & FILE_ATTRIBUTE_DIRECTORY) == default;
    }
}