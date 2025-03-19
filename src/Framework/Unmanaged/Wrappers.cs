using System;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Unmanaged;

static partial class Wrappers
{
    internal static T Create<T>(in Guid clsid, in Guid iid)
    {
        CoCreateInstance(clsid, default, CLSCTX_INPROC_SERVER, iid, out var ppv);
        return (T)ppv;
    }
}