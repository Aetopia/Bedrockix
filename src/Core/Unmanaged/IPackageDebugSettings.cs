using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Bedrockix.Unmanaged;

[Guid("F27C3930-8029-4AD1-94E3-3DBA417810C1"), GeneratedComInterface(StringMarshalling = StringMarshalling.Utf16)]
partial interface IPackageDebugSettings
{
    void EnableDebugging(string packageFullName, nint debuggerCommandLine, nint environment);
    void DisableDebugging(string packageFullName);
    void Suspend(nint packageFullName);
    void Resume(nint packageFullName);
    void TerminateAllProcesses(string packageFullName);
}