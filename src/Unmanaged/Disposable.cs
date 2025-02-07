using System;
using System.Runtime.InteropServices;

namespace Bedrockix.Unmanaged;

struct Disposable<T> : IDisposable
{
    T Value;

    readonly bool Unmanaged;

    internal Action<T> Action;

    internal T Object { set { if (Unmanaged) Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error()); Value = value; } }

    internal Disposable(bool value) => Unmanaged = value;

    public static implicit operator T(Disposable<T> value) => value.Value;

    public readonly void Dispose() => Action(Value);
}