using System.Security;
using System.Runtime.InteropServices;
using static Bedrockix.Unmanaged.Constants;
using Bedrockix.Unmanaged.Types;

[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32)]

namespace Bedrockix.Unmanaged;

[SuppressUnmanagedCodeSecurity]
unsafe static class Native
{
    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern bool ParseApplicationUserModelId(string applicationUserModelId = default, in int packageFamilyNameLength = PACKAGE_RELATIVE_APPLICATION_ID_MAX_LENGTH, char* packageFamilyName = default, in int packageRelativeApplicationIdLength = PACKAGE_RELATIVE_APPLICATION_ID_MAX_LENGTH, char* packageRelativeApplicationId = default);

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern int GetPackagesByPackageFamily(string packageFamilyName, in int count = default, nint packageFullNames = default, in int bufferLength = default, nint buffer = default);

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    internal static extern int CompareStringOrdinal(char* lpString1 = default, int cchCount1 = -1, char* lpString2 = default, int cchCount2 = -1, bool bIgnoreCase = default);

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    internal static extern bool GetApplicationUserModelId(nint hProcess, in int applicationUserModelIdLength = APPLICATION_USER_MODEL_ID_MAX_LENGTH, char* applicationUserModelId = default);

    [DllImport("User32", ExactSpelling = true, SetLastError = true)]
    internal static extern void GetWindowThreadProcessId(nint hWnd, out int lpdwProcessId);

    [DllImport("User32", ExactSpelling = true, EntryPoint = "FindWindowExW", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern nint FindWindowEx(nint hWndParent = default, nint hWndChildAfter = default, string lpszClass = "MSCTFIME UI", nint lpszWindow = default);

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    internal static extern bool GetFileInformationByHandleEx(nint hFile, int FileInformationClass, out FILE_STANDARD_INFO lpFileInformation, int dwBufferSize);

    [DllImport("Kernel32", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern nint CreateFile2(char* lpFileName, int dwDesiredAccess = default, int dwShareMode = FILE_SHARE_DELETE, int dwCreationDisposition = OPEN_EXISTING, nint pCreateExParams = default);

    [DllImport("Kernel32", SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryExW", ExactSpelling = true)]
    internal static extern nint LoadLibraryEx(string lpLibFileName, nint hFile = default, int dwFlags = DONT_RESOLVE_DLL_REFERENCES);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern bool FreeLibrary(nint hLibModule);

    [DllImport("Kernel32", SetLastError = true, EntryPoint = "GetModuleHandleW", CharSet = CharSet.Unicode, ExactSpelling = true)]
    internal static extern nint GetModuleHandle(string lpModuleName);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern nint OpenProcess(int dwDesiredAccess = default, bool bInheritHandle = default, int dwProcessId = default);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern nint CreateRemoteThread(nint hProcess, nint lpThreadAttributes = default, int dwStackSize = default, nint lpStartAddress = default, nint lpParameter = default, int dwCreationFlags = default, nint lpThreadId = default);

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