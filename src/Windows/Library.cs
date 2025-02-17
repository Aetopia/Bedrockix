using System;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Bedrockix.Windows;

readonly struct Library : IDisposable
{
    readonly nint Handle;

    public static implicit operator nint(Library value) => value.Handle;

    internal Library(string value)
    {
        if ((Handle = LoadLibraryEx(value, default, LOAD_LIBRARY_SEARCH_SYSTEM32)) == default)
            throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    internal readonly nint this[string value]
    {
        get
        {
            var address = GetProcAddress(Handle, value);
            return address != default ? address : throw new Win32Exception(Marshal.GetLastWin32Error());
        }
    }

    public void Dispose() => FreeLibrary(Handle);
}