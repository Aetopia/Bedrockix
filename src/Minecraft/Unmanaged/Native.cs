using System.Runtime.InteropServices;

namespace  Bedrockix.Minecraft.Unmanaged;

static partial class Native
{
    [LibraryImport("Kernel32", SetLastError = true)]
    internal static partial nint OpenProcess(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

    [LibraryImport("Kernel32", SetLastError = true)]
    internal static partial void WaitForSingleObject(nint hHandle, int dwMilliseconds);

    [LibraryImport("Kernel32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial void CloseHandle(nint hObject);
}