using System;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

readonly ref struct Process : IDisposable
{
    internal readonly int Id;

    internal readonly nint Handle;

    public void Dispose() => CloseHandle(Handle);

    internal Process(int value) => Handle = OpenProcess(PROCESS_ALL_ACCESS, default, Id = value);

    internal bool Running => WaitForSingleObject(Handle, default) is WAIT_TIMEOUT;
}
