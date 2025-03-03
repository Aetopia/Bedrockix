using System;
using static Bedrockix.Unmanaged.Native;

namespace Bedrockix.Windows;

readonly struct Event : IDisposable
{
    readonly nint Handle;

    public static implicit operator nint(Event @this) => @this.Handle;

    public Event() => Handle = CreateEvent(default, default, default, default);

    public readonly void Dispose() => CloseHandle(Handle);

    internal readonly void Set() => SetEvent(Handle);
}