using System.Threading;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Unmanaged;

unsafe static class Safe
{
    internal static Handle? CreateFile(char* lpFileName)
    {
        var @this = Unsafe.CreateFile(lpFileName, default, FILE_SHARE_DELETE, OPEN_EXISTING, default);
        return @this is INVALID_HANDLE_VALUE ? default : new(@this);
    }

    internal static bool GetFileInformationByHandleEx(in Handle hFile)
    {
        Unsafe.GetFileInformationByHandleEx(hFile, FILE_INFO_BY_HANDLE_CLASS.FileStandardInfo, out var @this, sizeof(FILE_STANDARD_INFO));
        return !@this.DeletePending;
    }

    internal static bool WaitForSingleObject(in Handle hHandle, bool dwMilliseconds) =>
    Unsafe.WaitForSingleObject(hHandle, dwMilliseconds ? 1 : 0) is WAIT_TIMEOUT;

    internal static void WaitForSingleObject(in Handle hHandle) =>
     _ = Unsafe.WaitForSingleObject(hHandle, Timeout.Infinite);

    internal static int GetWindowThreadProcessId(nint hWnd)
    {
        Unsafe.GetWindowThreadProcessId(hWnd, out var @this);
        return @this;
    }

    internal static nint OpenProcess(int dwProcessId) =>
    Unsafe.OpenProcess(PROCESS_ALL_ACCESS, default, dwProcessId);

    internal static nint FindWindowEx(nint hWnd) =>
    Unsafe.FindWindowEx(default, hWnd, "MSCTFIME UI", default);

    internal static PackageFamilyName ParseApplicationUserModelId(in ApplicationUserModelId applicationUserModelId)
    {
        Unsafe.ParseApplicationUserModelId(applicationUserModelId, PackageFamilyName.Length, out var @this, PackageFamilyName.Length, default);
        return @this;
    }

    internal static bool GetApplicationUserModelId(in Process hProcess, out ApplicationUserModelId applicationUserModelId) =>
    !Unsafe.GetApplicationUserModelId(hProcess, ApplicationUserModelId.Length, applicationUserModelId);

    internal static bool CompareStringOrdinal(in ApplicationUserModelId lpString1, in ApplicationUserModelId lpString2) =>
    Unsafe.CompareStringOrdinal(lpString1, -1, lpString2, -1, true) is CSTR_EQUAL;

    internal static bool LoadLibraryEx(string lpLibFileName) =>
    Unsafe.FreeLibrary(Unsafe.LoadLibraryEx(lpLibFileName, default, DONT_RESOLVE_DLL_REFERENCES));

    internal static Address VirtualAllocEx(in Process hProcess, int dwSize) =>
    new(hProcess, Unsafe.VirtualAllocEx(hProcess, default, dwSize, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE), dwSize);

    internal static nint GetProcAddress() =>
    Unsafe.GetProcAddress(Unsafe.GetModuleHandle("Kernel32"), "LoadLibraryW");

    internal static bool GetPackagesByPackageFamily(in PackageFamilyName packageFamilyName)
    {
        Unsafe.GetPackagesByPackageFamily(packageFamilyName, out var @this, default, out _, default);
        return @this;
    }
}