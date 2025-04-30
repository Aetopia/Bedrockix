using System.Security;
using System.Runtime.InteropServices;
using static Bedrockix.Unmanaged.Constants;

[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32)]

namespace Bedrockix.Unmanaged;

[SuppressUnmanagedCodeSecurity]
unsafe static class Unsafe
{
    [DllImport("Kernel32", ExactSpelling = true, CharSet = CharSet.Unicode, EntryPoint = "lstrcpyW")]
    internal static extern void lstrcpy(in ApplicationUserModelId lpString1, string lpString2);

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    internal static extern void ParseApplicationUserModelId(in ApplicationUserModelId applicationUserModelId, in int packageFamilyNameLength, out PackageFamilyName packageFamilyName, in int packageRelativeApplicationIdLength, in PackageFamilyName packageRelativeApplicationId);

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    internal static extern void GetPackagesByPackageFamily(in PackageFamilyName packageFamilyName, out bool count, nint packageFullNames, in int bufferLength, nint buffer);

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    internal static extern int CompareStringOrdinal(in ApplicationUserModelId lpString1, int cchCount1, in ApplicationUserModelId lpString2, int cchCount2, bool bIgnoreCase = true);

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    internal static extern bool GetApplicationUserModelId(nint hProcess, in int applicationUserModelIdLength, in ApplicationUserModelId applicationUserModelId);

    [DllImport("User32", ExactSpelling = true, SetLastError = true)]
    internal static extern void GetWindowThreadProcessId(nint hWnd, out int lpdwProcessId);

    [DllImport("User32", ExactSpelling = true, EntryPoint = "FindWindowExW", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern nint FindWindowEx(nint hWndParent, nint hWndChildAfter, string lpszClass, nint lpszWindow);

    [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    internal static extern void GetFileInformationByHandleEx(nint hFile, FILE_INFO_BY_HANDLE_CLASS FileInformationClass, out FileStandardInfo lpFileInformation, int dwBufferSize);

    [DllImport("Kernel32", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true, EntryPoint = "CreateFile2")]
    internal static extern nint CreateFile(char* lpFileName, int dwDesiredAccess, int dwShareMode, int dwCreationDisposition, nint pCreateExParams);

    [DllImport("Kernel32", SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryExW", ExactSpelling = true)]
    internal static extern nint LoadLibraryEx(string lpLibFileName, nint hFile, int dwFlags);

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