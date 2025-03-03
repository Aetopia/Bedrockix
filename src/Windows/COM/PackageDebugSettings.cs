using Bedrockix.Unmanaged.COM;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Bedrockix.Windows.COM;

[ComImport, Guid("B1AEC16F-2383-4852-B0E9-8F0B1DC66B4D")]
sealed class PackageDebugSettings : IPackageDebugSettings
{
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void EnableDebugging([MarshalAs(UnmanagedType.LPWStr)] string packageFullName, nint debuggerCommandLine, nint environment);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void DisableDebugging([MarshalAs(UnmanagedType.LPWStr)] string packageFullName);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void Suspend(nint packageFullName);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void Resume(nint packageFullName);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void TerminateAllProcesses([MarshalAs(UnmanagedType.LPWStr)] string packageFullName);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void SetTargetSessionId(ulong sessionId);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void EnumerateBackgroundTasks(nint packageFullName, nint taskCount, nint taskIds, nint taskNames);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void ActivateBackgroundTask(nint taskId);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void StartServicing([MarshalAs(UnmanagedType.LPWStr)] string packageFullName);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void StopServicing([MarshalAs(UnmanagedType.LPWStr)] string packageFullName);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void StartSessionRedirection(nint packageFullName, ulong sessionId);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void StopSessionRedirection(nint packageFullName);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void GetPackageExecutionState(nint packageFullName, nint packageExecutionState);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void RegisterForPackageStateChanges(nint packageFullName, nint pPackageExecutionStateChangeNotification, nint pdwCookie);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void UnregisterForPackageStateChanges(uint dwCookie);
}