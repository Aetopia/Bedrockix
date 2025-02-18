using System;
using System.ComponentModel;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;
using System.Runtime.InteropServices;

namespace Bedrockix.Windows;

readonly struct Library : IDisposable
{
    readonly nint Object;

    public static implicit operator nint(Library value) => value.Object;

    internal Library(string value)
    {
        if ((Object = LoadLibraryEx(value, default, LOAD_LIBRARY_SEARCH_SYSTEM32)) == default)
            throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    internal readonly nint this[string value]
    {
        get
        {
            var @object = GetProcAddress(Object, value);
            return @object != default ? @object : throw new Win32Exception(Marshal.GetLastWin32Error());
        }
    }

    public void Dispose() => FreeLibrary(Object);
}