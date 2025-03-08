using System.Runtime.InteropServices;

[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32)]

namespace Bedrockix.Unmanaged;

static partial class Native
{
    [DllImport("Kernel32", SetLastError = true)]
    internal static extern bool GetExitCodeProcess(nint hProcess, out int lpExitCode);

    [LibraryImport("Kernel32", SetLastError = true, EntryPoint = "GetBinaryTypeW", StringMarshalling = StringMarshalling.Utf16)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool GetBinaryType(string lpApplicationName, out int lpBinaryType);

    [LibraryImport("Kernel32", SetLastError = true, EntryPoint = "LoadLibraryExW", StringMarshalling = StringMarshalling.Utf16)]
    internal static partial nint LoadLibraryEx(string lpLibFileName, nint hFile, int dwFlags);

    [LibraryImport("Kernel32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool FreeLibrary(nint hLibModule);

    [LibraryImport("Kernel32", SetLastError = true, StringMarshalling = StringMarshalling.Utf16, EntryPoint = "GetModuleHandleW")]
    internal static partial nint GetModuleHandle(string lpModuleName);

    [LibraryImport("Kernel32", SetLastError = true)]
    internal static partial nint OpenProcess(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

    [LibraryImport("Kernel32", SetLastError = true, EntryPoint = "CreateEventW")]
    internal static partial nint CreateEvent(nint lpEventAttributes, [MarshalAs(UnmanagedType.Bool)] bool bManualReset, [MarshalAs(UnmanagedType.Bool)] bool bInitialState, nint lpName);

    [LibraryImport("Kernel32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool SetEvent(nint hEvent);

    [LibraryImport("Kernel32", SetLastError = true)]
    internal static partial nint CreateRemoteThread(nint hProcess, nint lpThreadAttributes, int dwStackSize, nint lpStartAddress, nint lpParameter, int dwCreationFlags, nint lpThreadId);

    [LibraryImport("Kernel32", SetLastError = true, StringMarshalling = StringMarshalling.Utf8)]
    internal static partial nint GetProcAddress(nint hModule, string lpProcName);

    [LibraryImport("Kernel32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool VirtualFreeEx(nint hProcess, nint lpAddress, int dwSize, int dwFreeType);

    [LibraryImport("Kernel32", SetLastError = true)]
    internal static partial nint VirtualAllocEx(nint hProcess, nint lpAddress, int dwSize, int flAllocationType, int flProtect);

    [LibraryImport("Kernel32", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool WriteProcessMemory(nint hProcess, nint lpBaseAddress, string lpBuffer, int nSize, nint lpNumberOfBytesWritten);

    [LibraryImport("Kernel32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool CloseHandle(nint hObject);

    [LibraryImport("Kernel32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal unsafe static partial bool WaitForMultipleObjects(int nCount, nint* lpHandles, [MarshalAs(UnmanagedType.Bool)] bool bWaitAll, int dwMilliseconds);

    [LibraryImport("Kernel32", SetLastError = true)]
    internal static partial int WaitForSingleObject(nint hHandle, int dwMilliseconds);

    [LibraryImport("Kernel32", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
    internal static partial int GetPackagesByPackageFamily(string packageFamilyName, out uint count, nint packageFullNames, out uint bufferLength, nint buffer);
}