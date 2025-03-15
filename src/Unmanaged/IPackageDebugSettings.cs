using System.Runtime.InteropServices;

namespace Bedrockix.Unmanaged;

[ComImport, Guid("B1AEC16F-2383-4852-B0E9-8F0B1DC66B4D")]
class PackageDebugSettings;

[Guid("F27C3930-8029-4AD1-94E3-3DBA417810C1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IPackageDebugSettings
{
    void EnableDebugging([MarshalAs(UnmanagedType.LPWStr)] string packageFullName, nint debuggerCommandLine, nint environment);

    void DisableDebugging([MarshalAs(UnmanagedType.LPWStr)] string packageFullName);

    void Suspend(nint packageFullName);

    void Resume(nint packageFullName);

    void TerminateAllProcesses([MarshalAs(UnmanagedType.LPWStr)] string packageFullName);

    void SetTargetSessionId(ulong sessionId);

    void EnumerateBackgroundTasks(nint packageFullName, nint taskCount, nint taskIds, nint taskNames);

    void ActivateBackgroundTask(nint taskId);

    void StartServicing([MarshalAs(UnmanagedType.LPWStr)] string packageFullName);

    void StopServicing([MarshalAs(UnmanagedType.LPWStr)] string packageFullName);

    void StartSessionRedirection(nint packageFullName, ulong sessionId);

    void StopSessionRedirection(nint packageFullName);

    void GetPackageExecutionState(nint packageFullName, nint packageExecutionState);

    void RegisterForPackageStateChanges(nint packageFullName, nint pPackageExecutionStateChangeNotification, nint pdwCookie);

    void UnregisterForPackageStateChanges(uint dwCookie);
}


