using System;
using System.Threading;
using static Bedrockix.Unmanaged.Native;

namespace Bedrockix.Windows;

readonly partial struct Handle : IDisposable
{
    readonly nint Object;

    internal Handle(nint value) => Object = value;

    public static implicit operator nint(Handle value) => value.Object;

    public void Dispose() => CloseHandle(Object);

    internal static void Single(nint value) => WaitForSingleObject(value, Timeout.Infinite);
}