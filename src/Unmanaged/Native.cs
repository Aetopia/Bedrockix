using System.Security;
using System.Runtime.InteropServices;
using static Bedrockix.Unmanaged.Constants;

[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32)]

namespace Bedrockix.Unmanaged;

[SuppressUnmanagedCodeSecurity]
static class Native
{
    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    internal static extern bool GetFileInformationByHandleEx(nint hFile, int FileInformationClass, out Types.FILE_STANDARD_INFO lpFileInformation, int dwBufferSize);

    [DllImport("Kernel32", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
    internal unsafe static extern nint CreateFile2(char* lpFileName, int dwDesiredAccess = default, int dwShareMode = FILE_SHARE_DELETE, int dwCreationDisposition = OPEN_EXISTING, nint pCreateExParams = default);

    [DllImport("Kernel32", SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryExW", ExactSpelling = true)]
    internal static extern nint LoadLibraryEx(string lpLibFileName, nint hFile = default, int dwFlags = DONT_RESOLVE_DLL_REFERENCES);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern bool FreeLibrary(nint hLibModule);

    [DllImport("Kernel32", SetLastError = true, EntryPoint = "GetModuleHandleW", CharSet = CharSet.Unicode, ExactSpelling = true)]
    internal static extern nint GetModuleHandle(string lpModuleName);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern nint OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern nint CreateRemoteThread(nint hProcess, nint lpThreadAttributes, int dwStackSize, nint lpStartAddress, nint lpParameter, int dwCreationFlags = default, nint lpThreadId = default);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Ansi)]
    internal static extern nint GetProcAddress(nint hModule, string lpProcName);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern bool VirtualFreeEx(nint hProcess, nint lpAddress, int dwSize = default, int dwFreeType = MEM_RELEASE);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern nint VirtualAllocEx(nint hProcess, nint lpAddress, int dwSize, int flAllocationType = MEM_COMMIT | MEM_RESERVE, int flProtect = PAGE_READWRITE);

    [DllImport("Kernel32", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    internal static extern void WriteProcessMemory(nint hProcess, nint lpBaseAddress, string lpBuffer, int nSize, nint lpNumberOfBytesWritten = default);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern void CloseHandle(nint hObject);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern int WaitForSingleObject(nint hHandle, int dwMilliseconds = default);
}