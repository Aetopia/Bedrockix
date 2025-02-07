using Bedrockix.Unmanaged;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Bedrockix.Windows;

static class Loader
{
    readonly static nint Address;

    static Loader()
    {
        using Disposable<nint> hModule = new(true)
        {
            Object = LoadLibraryEx("Kernel32.dll", default, LOAD_LIBRARY_SEARCH_SYSTEM32),
            Action = (_) => FreeLibrary(_)
        };

        Address = GetProcAddress(hModule, "LoadLibraryW");
    }

    internal static void Load(nint handle, string path)
    {
        FileInfo info = new(path);
        if (!info.Exists) throw new FileNotFoundException();

        var size = sizeof(char) * (path.Length + 1);

        using Disposable<nint> address = new(true)
        {
            Object = VirtualAllocEx(handle, default, size, MEM_COMMIT | MEM_RELEASE, PAGE_EXECUTE_READWRITE),
            Action = (_) => VirtualFreeEx(handle, _, default, MEM_RELEASE)
        };

        using Disposable<nint> buffer = new()
        {
            Object = Marshal.StringToHGlobalUni(path),
            Action = Marshal.FreeHGlobal
        };

        if (!WriteProcessMemory(handle, address, buffer, size, default))
            Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());

        using Disposable<nint> thread = new(true)
        {
            Object = CreateRemoteThread(handle, default, default, Address, address, default, default),
            Action = (_) => CloseHandle(_)
        };

        WaitForSingleObject(thread, Timeout.Infinite);
    }

    internal static void Load(int processId, string path)
    {
        using Disposable<nint> handle = new(true)
        {
            Object = OpenProcess(PROCESS_ALL_ACCESS, false, processId),
            Action = (_) => CloseHandle(_)
        };
        Load(handle, path);
    }
}