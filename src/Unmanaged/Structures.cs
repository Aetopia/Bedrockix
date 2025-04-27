using System.Runtime.InteropServices;
using static Bedrockix.Unmanaged.Native;

namespace Bedrockix.Unmanaged.Structures;

[StructLayout(LayoutKind.Sequential)]
readonly ref struct FILE_STANDARD_INFO
{
    internal readonly long AllocationSize, EndOfFile; internal readonly uint NumberOfLinks;
    [MarshalAs(UnmanagedType.U1)] internal readonly bool DeletePending, Directory;
}

[StructLayout(default(LayoutKind), Size = sizeof(char) * Length)]
readonly struct ApplicationUserModelId
{
    internal const int Length = 130;
    internal ApplicationUserModelId(string @this) => lstrcpy(this, @this);
}

[StructLayout(default(LayoutKind), Size = sizeof(char) * Length)]
readonly struct PackageFamilyName { internal const int Length = 65; }
