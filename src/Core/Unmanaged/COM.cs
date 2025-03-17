using System;
using System.Runtime.InteropServices;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;
using System.Runtime.InteropServices.Marshalling;

namespace Bedrockix.Unmanaged;

static class COM
{
    static readonly StrategyBasedComWrappers Wrappers = new();

    internal static T Create<T>(in Guid clsid, in Guid iid)
    {
        Marshal.ThrowExceptionForHR(CoCreateInstance(clsid, default, CLSCTX_INPROC_SERVER, iid, out var ppv));
        return (T)Wrappers.GetOrCreateObjectForComInstance(ppv, CreateObjectFlags.None);
    }
}