using System;
using static Bedrockix.Unmanaged.Native ;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

readonly ref struct Instance(int value) : IDisposable
{
    internal readonly int Id = value;

    internal readonly nint Handle = OpenProcess(PROCESS_ALL_ACCESS, dwProcessId: value);

    public void Dispose() => CloseHandle(Handle);

    internal bool this[bool value] => WaitForSingleObject(Handle, value ? 1 : 0) is WAIT_TIMEOUT;
}
