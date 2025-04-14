using System;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Windows;

readonly ref struct Handle
{
    internal readonly ref struct Process : IDisposable
    {
        internal readonly nint Handle;

        internal readonly int Id;

        internal Process(int value) => Handle = OpenProcess(PROCESS_ALL_ACCESS, default, Id = value);

        internal bool Running => GetExitCodeProcess(Handle, out var value) && value is STATUS_PENDING;

        public void Dispose() => CloseHandle(Handle);
    }
}