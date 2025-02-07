using System;
using System.Runtime.InteropServices;

namespace Bedrockix.Unmanaged;

readonly struct Disposable<T> : IDisposable
{
    readonly T Value;

    readonly Action<T> Action;

    public static implicit operator T(Disposable<T> value) => value.Value;

    internal Disposable(T value, Action<T> action)
    {
        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        Value = value;
        Action = action;
    }

    public void Dispose() => Action(Value);
}