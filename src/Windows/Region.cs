using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

readonly struct Region : IDisposable
{
    readonly nint Handle, Value;

    public static implicit operator nint(Region @this) => @this.Value;

    internal Region(nint handle, int size)
    {
        if ((Value = VirtualAllocEx(Handle = handle, default, size, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE)) == default)
            throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    public void Dispose() => VirtualFreeEx(Handle, Value, default, MEM_RELEASE);
}