using System;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

readonly struct Process : IDisposable
{
    readonly nint Handle;

    internal readonly int Id;

    public static implicit operator nint(Process @this) => @this.Handle;

    internal Process(int value) => Handle = OpenProcess(PROCESS_ALL_ACCESS, default, Id = value);

    public void Dispose() => CloseHandle(Handle);

    internal bool Running => GetExitCodeProcess(Handle, out var value) && value is STATUS_PENDING;
}