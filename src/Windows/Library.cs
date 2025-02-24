using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

readonly struct Library : IDisposable
{
    readonly nint Module;

    internal Library(string value)
    {
        if ((Module = LoadLibraryEx(value, default, LOAD_LIBRARY_SEARCH_SYSTEM32)) == default)
            throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    internal readonly nint this[string value]
    {
        get
        {
            var @object = GetProcAddress(Module, value);
            return @object != default ? @object : throw new Win32Exception(Marshal.GetLastWin32Error());
        }
    }

    public void Dispose() => FreeLibrary(Module);
}