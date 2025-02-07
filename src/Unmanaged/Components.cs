using System;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Unmanaged;

static class Components
{
    internal struct Component<T> { internal Guid IID, CLSID; }

    internal static Component<IPackageDebugSettings> PackageDebugSettings = new()
    {
        IID = new("F27C3930-8029-4AD1-94E3-3DBA417810C1"),
        CLSID = new("B1AEC16F-2383-4852-B0E9-8F0B1DC66B4D")
    };

    internal static Component<IApplicationActivationManager> ApplicationActivationManager = new()
    {
        IID = new("2E941141-7F97-4756-BA1D-9DECDE894A3D"),
        CLSID = new("45BA127D-10A8-46EA-8AB7-56EA9078943C")
    };

    internal static T Create<T>(this Component<T> source)
    {
        CoCreateInstance(ref source.CLSID, default, CLSCTX_INPROC_SERVER, ref source.IID, out var ppv);
        return (T)ppv;
    }
}