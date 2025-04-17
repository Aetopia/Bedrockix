using System.Security;
using System.Runtime.InteropServices;

[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32)]

namespace Bedrockix.Unmanaged;

[SuppressUnmanagedCodeSecurity]
static class Native
{
    [DllImport("Shell32", CharSet = CharSet.Unicode, ExactSpelling = true, EntryPoint = "SHGetFileInfoW")]
    internal static extern bool SHGetFileInfo(string pszPath, int dwFileAttributes, nint psfi, int cbFileInfo, int uFlags);

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    internal static extern bool GetFileInformationByHandleEx(nint hFile, int FileInformationClass, out Types.FILE_STANDARD_INFO lpFileInformation, int dwBufferSize);

    [DllImport("Kernel32", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
    internal unsafe static extern nint CreateFile2(string lpFileName, int dwDesiredAccess, int dwShareMode, int dwCreationDisposition, nint pCreateExParams);

    [DllImport("Kernel32", SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryExW", ExactSpelling = true)]
    internal unsafe static extern nint LoadLibraryEx(string lpLibFileName, nint hFile, int dwFlags);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern bool FreeLibrary(nint hLibModule);

    [DllImport("Kernel32", SetLastError = true, EntryPoint = "GetModuleHandleW", CharSet = CharSet.Unicode, ExactSpelling = true)]
    internal static extern nint GetModuleHandle(string lpModuleName);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern nint OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern nint CreateRemoteThread(nint hProcess, nint lpThreadAttributes, int dwStackSize, nint lpStartAddress, nint lpParameter, int dwCreationFlags, nint lpThreadId);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Ansi)]
    internal static extern nint GetProcAddress(nint hModule, string lpProcName);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern bool VirtualFreeEx(nint hProcess, nint lpAddress, int dwSize, int dwFreeType);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern nint VirtualAllocEx(nint hProcess, nint lpAddress, int dwSize, int flAllocationType, int flProtect);

    [DllImport("Kernel32", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    internal static extern void WriteProcessMemory(nint hProcess, nint lpBaseAddress, string lpBuffer, int nSize, nint lpNumberOfBytesWritten);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern void CloseHandle(nint hObject);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern int WaitForSingleObject(nint hHandle, int dwMilliseconds);
}