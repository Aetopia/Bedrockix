using System;
using System.Runtime.InteropServices;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Unmanaged;

[ComImport, Guid("B1AEC16F-2383-4852-B0E9-8F0B1DC66B4D")] class PackageDebugSettings;

[ComImport, Guid("45BA127D-10A8-46EA-8AB7-56EA9078943C")] class ApplicationActivationManager;

[Guid("2E941141-7F97-4756-BA1D-9DECDE894A3D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IApplicationActivationManager { void ActivateApplication(in ApplicationUserModelId appUserModelId, nint arguments, ACTIVATEOPTIONS options, out int processId); }

[Guid("F27C3930-8029-4AD1-94E3-3DBA417810C1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IPackageDebugSettings
{
    void EnableDebugging([MarshalAs(UnmanagedType.LPWStr)] string packageFullName, nint debuggerCommandLine = default, nint environment = default);
    void DisableDebugging([MarshalAs(UnmanagedType.LPWStr)] string packageFullName); void Suspend(nint _); void Resume(nint _);
    void TerminateAllProcesses([MarshalAs(UnmanagedType.LPWStr)] string packageFullName);
}