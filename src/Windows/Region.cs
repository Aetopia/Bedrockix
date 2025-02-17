using System;
using System.Runtime.InteropServices;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;
using System.ComponentModel;

namespace Bedrockix.Windows;

readonly struct Region : IDisposable
{
    readonly nint Handle, Address;

    public static implicit operator nint(Region value) => value.Address;

    internal Region(nint handle, int size)
    {
        if ((Address = VirtualAllocEx(Handle = handle, default, size, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE)) == default)
            throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    public void Dispose() => VirtualFreeEx(Handle, Address, default, MEM_RELEASE);
}