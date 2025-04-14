using System.Runtime.InteropServices;

namespace Bedrockix.Unmanaged.Types;

[StructLayout(LayoutKind.Sequential)]
internal readonly ref struct FILE_STANDARD_INFO
{
    internal readonly long AllocationSize, EndOfFile;

    internal readonly uint NumberOfLinks;

    [MarshalAs(UnmanagedType.U1)]
    internal readonly bool DeletePending, Directory;
}