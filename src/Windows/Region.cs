using System;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

readonly struct Region : IDisposable
{
    readonly nint Handle, Address;

    public static implicit operator nint(Region @this) => @this.Address;

    internal Region(nint handle, string value)
    {
        var _ = sizeof(char) * (value.Length + 1);
        WriteProcessMemory(Handle, Address = VirtualAllocEx(Handle = handle, default, _, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE), value, _, default);
    }

    public void Dispose() => VirtualFreeEx(Handle, Address, default, MEM_RELEASE);
}