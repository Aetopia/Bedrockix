using System.Threading;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Unmanaged;

unsafe static class Safe
{
    internal static Handle? CreateFile(char* lpFileName)
    {
        var @this = Unsafe.CreateFile(lpFileName, default, FILE_SHARE_DELETE, OPEN_EXISTING, default);
        return @this is INVALID_HANDLE_VALUE ? null : new(@this);
    }

    internal static bool GetFileInformationByHandleEx(in Handle? hFile)
    {
        Unsafe.GetFileInformationByHandleEx((Handle)hFile, FILE_INFO_BY_HANDLE_CLASS.FileStandardInfo, out var @this, sizeof(FileStandardInfo));
        return @this.DeletePending;
    }

    internal static bool WaitForSingleObject(in Handle hHandle, bool dwMilliseconds)
    {
        return Unsafe.WaitForSingleObject(hHandle, dwMilliseconds ? 1 : 0) is WAIT_TIMEOUT;
    }

    internal static void WaitForSingleObject(in Handle hHandle)
    {
        Unsafe.WaitForSingleObject(hHandle, Timeout.Infinite);
    }

    internal static int GetWindowThreadProcessId(nint hWnd)
    {
        Unsafe.GetWindowThreadProcessId(hWnd, out var @this);
        return @this;
    }

    internal static nint OpenProcess(int dwProcessId)
    {
        return Unsafe.OpenProcess(PROCESS_ALL_ACCESS, default, dwProcessId);
    }

    internal static nint FindWindowEx(nint hWndChildAfter)
    {
        return Unsafe.FindWindowEx(default, hWndChildAfter, "MSCTFIME UI", default);
    }

    internal static PackageFamilyName ParseApplicationUserModelId(in ApplicationUserModelId applicationUserModelId)
    {
        Unsafe.ParseApplicationUserModelId(applicationUserModelId, PackageFamilyName.Length, out var @this, PackageFamilyName.Length, default);
        return @this;
    }

    internal static bool GetApplicationUserModelId(in Process hProcess, out ApplicationUserModelId applicationUserModelId)
    {
        return !Unsafe.GetApplicationUserModelId(hProcess, ApplicationUserModelId.Length, out applicationUserModelId);
    }

    internal static bool CompareStringOrdinal(in ApplicationUserModelId lpString1, in ApplicationUserModelId lpString2)
    {
        return Unsafe.CompareStringOrdinal(lpString1, -1, lpString2, -1, true) is CSTR_EQUAL;
    }

    internal static bool LoadLibraryEx(string lpLibFileName)
    {
        return Unsafe.FreeLibrary(Unsafe.LoadLibraryEx(lpLibFileName, default, DONT_RESOLVE_DLL_REFERENCES));
    }

    internal static nint GetProcAddress()
    {
        return Unsafe.GetProcAddress(Unsafe.GetModuleHandle("Kernel32"), "LoadLibraryW");
    }

    internal static bool GetPackagesByPackageFamily(in PackageFamilyName packageFamilyName)
    {
        Unsafe.GetPackagesByPackageFamily(packageFamilyName, out var @this, default, out _, default);
        return @this;
    }
}