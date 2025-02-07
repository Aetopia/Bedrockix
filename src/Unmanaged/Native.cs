using System;
using System.Runtime.InteropServices;

[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32)]

namespace Bedrockix.Unmanaged;

static class Native
{
    [DllImport("Kernel32", SetLastError = true)]
    internal static extern nint OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern int WaitForSingleObject(nint hHandle, int dwMilliseconds);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern bool CloseHandle(nint hObject);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern nint VirtualAllocEx(nint hProcess, nint lpAddress, int dwSize, int flAllocationType, int flProtect);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern bool WriteProcessMemory(nint hProcess, nint lpBaseAddress, nint lpBuffer, int nSize, nint lpNumberOfBytesWritten);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern nint CreateRemoteThread(nint hProcess, nint lpThreadAttributes, int dwStackSize, nint lpStartAddress, nint lpParameter, int dwCreationFlags, nint lpThreadId);

    [DllImport("Kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern nint LoadLibraryEx([MarshalAs(UnmanagedType.LPWStr)] string lpLibFileName, nint hFile, int dwFlags);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern bool FreeLibrary(nint hLibModule);

    [DllImport("Kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
    internal static extern nint GetProcAddress(nint hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern bool VirtualFreeEx(nint hProcess, nint lpAddress, int dwSize, int dwFreeType);

    [DllImport("Ole32")]
    internal static extern void CoCreateInstance(ref Guid rclsid, nint pUnkOuter, int dwClsContext, ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);
}