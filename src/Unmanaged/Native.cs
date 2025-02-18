using System;
using System.Runtime.InteropServices;

[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32)]

namespace Bedrockix.Unmanaged;

static partial class Native
{
    [LibraryImport("Kernel32", SetLastError = true)]
    internal static partial nint OpenProcess(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

    [LibraryImport("Kernel32", SetLastError = true, EntryPoint = "LoadLibraryExW")]
    internal static partial nint LoadLibraryEx([MarshalAs(UnmanagedType.LPWStr)] string lpLibFileName, nint hFile, int dwFlags);

    [LibraryImport("Kernel32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool FreeLibrary(nint hLibModule);

    [LibraryImport("Kernel32", SetLastError = true)]
    internal static partial nint CreateRemoteThread(nint hProcess, nint lpThreadAttributes, int dwStackSize, nint lpStartAddress, nint lpParameter, int dwCreationFlags, nint lpThreadId);

    [LibraryImport("Kernel32", SetLastError = true)]
    internal static partial nint GetProcAddress(nint hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

    [LibraryImport("Kernel32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool VirtualFreeEx(nint hProcess, nint lpAddress, int dwSize, int dwFreeType);

    [LibraryImport("Kernel32", SetLastError = true)]
    internal static partial nint VirtualAllocEx(nint hProcess, nint lpAddress, int dwSize, int flAllocationType, int flProtect);

    [LibraryImport("Kernel32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool WriteProcessMemory(nint hProcess, nint lpBaseAddress, nint lpBuffer, int nSize, nint lpNumberOfBytesWritten);

    [LibraryImport("Kernel32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool CloseHandle(nint hObject);

    [LibraryImport("Kernel32", SetLastError = true)]
    internal static partial int WaitForMultipleObjects(int nCount, ReadOnlySpan<nint> lpHandles, [MarshalAs(UnmanagedType.Bool)] bool bWaitAll, int dwMilliseconds);

    [LibraryImport("Kernel32", SetLastError = true)]
    internal static partial int WaitForSingleObject(nint hHandle, int dwMilliseconds);

    [LibraryImport("Kernel32", SetLastError = true)]
    internal static partial int GetPackagesByPackageFamily([MarshalAs(UnmanagedType.LPWStr)] string packageFamilyName, out uint count, nint packageFullNames, out uint bufferLength, nint buffer);
}