using System.Runtime.InteropServices;

namespace Bedrockix.Unmanaged.Structures;

[StructLayout(LayoutKind.Sequential)]
readonly ref struct FILE_STANDARD_INFO
{
    internal readonly long AllocationSize, EndOfFile; internal readonly uint NumberOfLinks;
    [MarshalAs(UnmanagedType.U1)] internal readonly bool DeletePending, Directory;
}

[StructLayout(default(LayoutKind), Size = sizeof(char) * Length)]
readonly struct ApplicationUserModelId { internal const int Length = 130; }

[StructLayout(default(LayoutKind), Size = sizeof(char) * Length)]
readonly struct PackageFamilyName { internal const int Length = 65; }
