using System;
using System.Runtime.InteropServices;

[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32)]

namespace Bedrockix.Unmanaged;

static class Native
{
    internal const int AO_NOERRORUI = 0x00000002;

    internal const int LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800;

    internal const int MEM_RELEASE = 0x00008000;

    internal const int PROCESS_ALL_ACCESS = 0X1FFFFF;

    internal const int MEM_COMMIT = 0x00001000;

    internal const int MEM_RESERVE = 0x00002000;

    internal const int PAGE_EXECUTE_READWRITE = 0x40;

    internal const int CLSCTX_INPROC_SERVER = 0x1;

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern nint OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern int WaitForSingleObject(nint hHandle, int dwMilliseconds);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern bool CloseHandle(nint hObject);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern nint VirtualAllocEx(nint hProcess, nint lpAddress, int dwSize, int flAllocationType, int flProtect);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern bool WriteProcessMemory(nint hProcess, nint lpBaseAddress, nint lpBuffer, int nSize, nint lpNumberOfBytesWritten);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern nint CreateRemoteThread(nint hProcess, nint lpThreadAttributes, int dwStackSize, nint lpStartAddress, nint lpParameter, int dwCreationFlags, nint lpThreadId);

    [DllImport("Kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern nint LoadLibraryEx([MarshalAs(UnmanagedType.LPWStr)] string lpLibFileName, nint hFile, int dwFlags);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern bool FreeLibrary(nint hLibModule);

    [DllImport("Kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
    internal static extern nint GetProcAddress(nint hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

    [DllImport("Kernel32", SetLastError = true)]
    internal static extern bool VirtualFreeEx(nint hProcess, nint lpAddress, int dwSize, int dwFreeType);

    [DllImport("Ole32")]
    internal static extern void CoCreateInstance(ref Guid rclsid, nint pUnkOuter, int dwClsContext, ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

    internal static Guid IID_IPackageDebugSettings = new("F27C3930-8029-4AD1-94E3-3DBA417810C1");

    internal static Guid CLSID_PackageDebugSettings = new("B1AEC16F-2383-4852-B0E9-8F0B1DC66B4D");

    internal static Guid IID_IApplicationActivationManager = new("2E941141-7F97-4756-BA1D-9DECDE894A3D");

    internal static Guid CLSID_ApplicationActivationManager = new("45BA127D-10A8-46EA-8AB7-56EA9078943C");
}