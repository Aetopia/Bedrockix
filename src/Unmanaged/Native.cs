using System.Security;
using System.Runtime.InteropServices;
using Bedrockix.Unmanaged.Structures;
using static Bedrockix.Unmanaged.Constants;

[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32)]

namespace Bedrockix.Unmanaged;

[SuppressUnmanagedCodeSecurity]
unsafe static partial class Native
{
    [DllImport("Kernel32", ExactSpelling = true, CharSet = CharSet.Unicode, EntryPoint = "lstrcpyW")]
    internal static extern nint lstrcpy(in APPLICATION_USER_MODEL_ID lpString1, string lpString2);

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    internal static extern void ParseApplicationUserModelId(nint applicationUserModelId, in int packageFamilyNameLength = PACKAGE_FAMILY_NAME_MAX_LENGTH, in PACKAGE_FAMILY_NAME packageFamilyName = default, in int packageRelativeApplicationIdLength = PACKAGE_FAMILY_NAME_MAX_LENGTH, in PACKAGE_FAMILY_NAME packageRelativeApplicationId = default);

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    internal static extern void GetPackagesByPackageFamily(in PACKAGE_FAMILY_NAME packageFamilyName, out bool count, nint packageFullNames = default, in int bufferLength = default, nint buffer = default);

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    internal static extern int CompareStringOrdinal(in APPLICATION_USER_MODEL_ID lpString1 = default, int cchCount1 = -1, in APPLICATION_USER_MODEL_ID lpString2 = default, int cchCount2 = -1, bool bIgnoreCase = true);

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    internal static extern bool GetApplicationUserModelId(nint hProcess, in int applicationUserModelIdLength = APPLICATION_USER_MODEL_ID_MAX_LENGTH, in APPLICATION_USER_MODEL_ID applicationUserModelId = default);

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
    internal static extern nint OpenProcess(int dwDesiredAccess = PROCESS_ALL_ACCESS, bool bInheritHandle = default, int dwProcessId = default);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern nint CreateRemoteThread(nint hProcess, nint lpThreadAttributes = default, int dwStackSize = default, nint lpStartAddress = default, nint lpParameter = default, int dwCreationFlags = default, nint lpThreadId = default);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Ansi)]
    internal static extern nint GetProcAddress(nint hModule, string lpProcName = "LoadLibraryW");

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern bool VirtualFreeEx(nint hProcess, nint lpAddress, int dwSize = default, int dwFreeType = MEM_RELEASE);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern nint VirtualAllocEx(nint hProcess, nint lpAddress = default, int dwSize = default, int flAllocationType = MEM_COMMIT | MEM_RESERVE, int flProtect = PAGE_READWRITE);

    [DllImport("Kernel32", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    internal static extern void WriteProcessMemory(nint hProcess, nint lpBaseAddress, string lpBuffer, int nSize, nint lpNumberOfBytesWritten = default);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern void CloseHandle(nint hObject);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern int WaitForSingleObject(nint hHandle, int dwMilliseconds);
}