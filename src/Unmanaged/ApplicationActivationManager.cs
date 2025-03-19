using System;

namespace Bedrockix.Unmanaged;

static class ApplicationActivationManager
{
    readonly static Guid CLSID = new("45BA127D-10A8-46EA-8AB7-56EA9078943C");

    readonly static Guid IID = new("2E941141-7F97-4756-BA1D-9DECDE894A3D");

    internal static IApplicationActivationManager Create() => Wrappers.Create<IApplicationActivationManager>(CLSID, IID);
}