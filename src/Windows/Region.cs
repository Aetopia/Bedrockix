using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

readonly struct Region : IDisposable
{
    readonly nint Value, Object;

    public static implicit operator nint(Region value) => value.Object;

    internal Region(nint value, int @object)
    {
        if ((Object = VirtualAllocEx(Value = value, default, @object, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE)) == default)
            throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    public void Dispose() => VirtualFreeEx(Value, Object, default, MEM_RELEASE);
}