using System;
using System.Threading;
using static Bedrockix.Unmanaged.Native;

namespace Bedrockix.Windows;

readonly partial struct Handle : IDisposable
{
    internal static void Any(params ReadOnlySpan<nint> value) => WaitForMultipleObjects(value.Length, value, false, Timeout.Infinite);
}