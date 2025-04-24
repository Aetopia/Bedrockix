using System;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

readonly struct Process : IDisposable
{
    public void Dispose() => CloseHandle(Handle);

    internal readonly int Id; internal readonly nint Handle;

    internal bool this[bool @this] => WaitForSingleObject(Handle, @this) is WAIT_TIMEOUT;

    internal Process(int @this) => Handle = OpenProcess(PROCESS_ALL_ACCESS, dwProcessId: Id = @this);

    internal Process(nint @this) { GetWindowThreadProcessId(@this, out var @params); Handle = OpenProcess(PROCESS_ALL_ACCESS, dwProcessId: Id = @params); }
}
