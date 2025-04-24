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
    internal static extern bool ParseApplicationUserModelId(nint applicationUserModelId, in int packageFamilyNameLength = PACKAGE_FAMILY_NAME_MAX_LENGTH, in PACKAGE_FAMILY_NAME packageFamilyName = new(), in int packageRelativeApplicationIdLength = PACKAGE_FAMILY_NAME_MAX_LENGTH, in PACKAGE_FAMILY_NAME packageRelativeApplicationId = new());

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern void GetPackagesByPackageFamily(in PACKAGE_FAMILY_NAME packageFamilyName, out bool count, nint packageFullNames = new(), in int bufferLength = new(), nint buffer = new());

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    internal static extern int CompareStringOrdinal(in APPLICATION_USER_MODEL_ID lpString1 = new(), int cchCount1 = -1, in APPLICATION_USER_MODEL_ID lpString2 = new(), int cchCount2 = -1, bool bIgnoreCase = true);

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    internal static extern bool GetApplicationUserModelId(nint hProcess, in int applicationUserModelIdLength = APPLICATION_USER_MODEL_ID_MAX_LENGTH, in APPLICATION_USER_MODEL_ID applicationUserModelId = new());

    [DllImport("User32", ExactSpelling = true, SetLastError = true)]
    internal static extern void GetWindowThreadProcessId(nint hWnd, out int lpdwProcessId);

    [DllImport("User32", ExactSpelling = true, EntryPoint = "FindWindowExW", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern nint FindWindowEx(nint hWndParent = new(), nint hWndChildAfter = new(), string lpszClass = "MSCTFIME UI", nint lpszWindow = new());

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    internal static extern bool GetFileInformationByHandleEx(nint hFile, int FileInformationClass, out FILE_STANDARD_INFO lpFileInformation, int dwBufferSize);

    [DllImport("Kernel32", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern nint CreateFile2(char* lpFileName, int dwDesiredAccess = new(), int dwShareMode = FILE_SHARE_DELETE, int dwCreationDisposition = OPEN_EXISTING, nint pCreateExParams = new());

    [DllImport("Kernel32", SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryExW", ExactSpelling = true)]
    internal static extern nint LoadLibraryEx(string lpLibFileName, nint hFile = new(), int dwFlags = DONT_RESOLVE_DLL_REFERENCES);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern bool FreeLibrary(nint hLibModule);

    [DllImport("Kernel32", SetLastError = true, EntryPoint = "GetModuleHandleW", CharSet = CharSet.Unicode, ExactSpelling = true)]
    internal static extern nint GetModuleHandle(string lpModuleName);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern nint OpenProcess(int dwDesiredAccess = PROCESS_ALL_ACCESS, bool bInheritHandle = new(), int dwProcessId = new());

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern nint CreateRemoteThread(nint hProcess, nint lpThreadAttributes = new(), int dwStackSize = new(), nint lpStartAddress = new(), nint lpParameter = new(), int dwCreationFlags = new(), nint lpThreadId = new());

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Ansi)]
    internal static extern nint GetProcAddress(nint hModule, string lpProcName = "LoadLibraryW");

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern bool VirtualFreeEx(nint hProcess, nint lpAddress, int dwSize = new(), int dwFreeType = MEM_RELEASE);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern nint VirtualAllocEx(nint hProcess, nint lpAddress, int dwSize, int flAllocationType = MEM_COMMIT | MEM_RESERVE, int flProtect = PAGE_READWRITE);

    [DllImport("Kernel32", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    internal static extern void WriteProcessMemory(nint hProcess, nint lpBaseAddress, string lpBuffer, int nSize, nint lpNumberOfBytesWritten = new());

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern void CloseHandle(nint hObject);

    [DllImport("Kernel32", SetLastError = true, ExactSpelling = true)]
    internal static extern int WaitForSingleObject(nint hHandle, int dwMilliseconds);
}