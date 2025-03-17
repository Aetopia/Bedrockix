using System.Runtime.InteropServices;

namespace Bedrockix.Unmanaged;

[Guid("F27C3930-8029-4AD1-94E3-3DBA417810C1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IPackageDebugSettings
{
    void EnableDebugging([MarshalAs(UnmanagedType.LPWStr)] string packageFullName, nint debuggerCommandLine, nint environment);
    void DisableDebugging([MarshalAs(UnmanagedType.LPWStr)] string packageFullName);
    void Suspend(nint packageFullName);
    void Resume(nint packageFullName);
    void TerminateAllProcesses([MarshalAs(UnmanagedType.LPWStr)] string packageFullName);
}