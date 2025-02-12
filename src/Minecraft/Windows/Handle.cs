using System.Threading;

namespace Bedrockix.Minecraft.Windows;

sealed class Handle : WaitHandle
{
    internal Handle(nint value) => SafeWaitHandle = new(value, true);
}