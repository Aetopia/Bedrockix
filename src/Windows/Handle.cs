using System;
using System.Threading;
using static Bedrockix.Unmanaged.Native;

namespace Bedrockix.Windows;

readonly struct Handle : IDisposable
{
    readonly nint Object;

    internal Handle(nint value) => Object = value;

    public static implicit operator nint(Handle value) => value.Object;

    public void Dispose() => CloseHandle(Object);
#if NET
    internal static void Any(params ReadOnlySpan<nint> value) => WaitForMultipleObjects(value.Length, value, false, Timeout.Infinite);
#elif NETFRAMEWORK
    internal static void Any(params nint[] value) => WaitForMultipleObjects(value.Length, value, false, Timeout.Infinite);
#endif
    internal static void Single(nint value) => WaitForSingleObject(value, Timeout.Infinite);
}