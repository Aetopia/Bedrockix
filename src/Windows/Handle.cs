using System;
using System.Threading;
using static Bedrockix.Unmanaged.Native;

namespace Bedrockix.Windows;

readonly struct Handle : IDisposable
{
    readonly nint Value;

    internal Handle(nint value) => Value = value;

    public static implicit operator nint(Handle @this) => @this.Value;

    public void Dispose() => CloseHandle(Value);

    internal static void Wait(nint value) => WaitForSingleObject(value, Timeout.Infinite);

    internal unsafe static void Wait(int count, nint* handles) => WaitForMultipleObjects(count, handles, false, Timeout.Infinite);
}