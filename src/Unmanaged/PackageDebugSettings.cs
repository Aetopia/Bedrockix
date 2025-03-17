using System;
using System.Runtime.InteropServices;

namespace Bedrockix.Unmanaged;

static class PackageDebugSettings
{
    readonly static Guid CLSID = new("B1AEC16F-2383-4852-B0E9-8F0B1DC66B4D");

    readonly static Guid IID = new("F27C3930-8029-4AD1-94E3-3DBA417810C1");

    internal static IPackageDebugSettings Create() => COM.Create<IPackageDebugSettings>(CLSID, IID);
}