using System.Runtime.InteropServices;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Unmanaged.Structures;

[StructLayout(LayoutKind.Sequential)]
readonly ref struct FILE_STANDARD_INFO
{
    internal readonly long AllocationSize, EndOfFile; internal readonly uint NumberOfLinks;
    [MarshalAs(UnmanagedType.U1)] internal readonly bool DeletePending, Directory;
}

[StructLayout(LayoutKind.Sequential, Size = sizeof(char) * APPLICATION_USER_MODEL_ID_MAX_LENGTH)]
readonly struct APPLICATION_USER_MODEL_ID;

[StructLayout(LayoutKind.Sequential, Size = sizeof(char) * PACKAGE_FAMILY_NAME_MAX_LENGTH)]
readonly struct PACKAGE_FAMILY_NAME;
