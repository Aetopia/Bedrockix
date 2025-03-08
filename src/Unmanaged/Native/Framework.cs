using System.Security;
using System.Runtime.InteropServices;

[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32)]

namespace Bedrockix.Unmanaged;

[SuppressUnmanagedCodeSecurity]
static class Native
{
    [DllImport("Kernel32", SetLastError = true)]
    internal static extern bool GetExitCodeProcess(nint hProcess, out int lpExitCode);

    [DllImport("Kernel32", SetLastError = true, CharSet = CharSet.Auto)]
    internal static extern bool GetBinaryType(string lpApplicationName, out int lpBinaryType);

    [DllImport("Kernel32", SetLastError = true, CharSet = CharSet.Auto)]
    internal static extern nint LoadLibraryEx(string lpLibFileName, nint hFile, int dwFlags);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern bool FreeLibrary(nint hLibModule);

    [DllImport("Kernel32", SetLastError = true, CharSet = CharSet.Auto)]
    internal static extern nint GetModuleHandle(string lpModuleName);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern nint OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern nint CreateRemoteThread(nint hProcess, nint lpThreadAttributes, int dwStackSize, nint lpStartAddress, nint lpParameter, int dwCreationFlags, nint lpThreadId);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern nint GetProcAddress(nint hModule, string lpProcName);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern bool VirtualFreeEx(nint hProcess, nint lpAddress, int dwSize, int dwFreeType);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern nint VirtualAllocEx(nint hProcess, nint lpAddress, int dwSize, int flAllocationType, int flProtect);

    [DllImport("Kernel32", SetLastError = true, CharSet = CharSet.Auto)]
    internal static extern bool WriteProcessMemory(nint hProcess, nint lpBaseAddress, string lpBuffer, int nSize, nint lpNumberOfBytesWritten);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern bool CloseHandle(nint hObject);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern int WaitForSingleObject(nint hHandle, int dwMilliseconds);

    [DllImport("Kernel32", SetLastError = true, CharSet = CharSet.Auto)]
    internal static extern int GetPackagesByPackageFamily(string packageFamilyName, out uint count, nint packageFullNames, out uint bufferLength, nint buffer);
}