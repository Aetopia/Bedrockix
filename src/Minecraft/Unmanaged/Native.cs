using System.Runtime.InteropServices;

namespace Bedrockix.Minecraft.Unmanaged;

static partial class Native
{
    [LibraryImport("Kernel32", SetLastError = true)]
    internal static partial nint OpenProcess(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);
}