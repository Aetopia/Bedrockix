using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Bedrockix.Unmanaged.Components;

[Guid("2E941141-7F97-4756-BA1D-9DECDE894A3D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IApplicationActivationManager
{
    void ActivateApplication([MarshalAs(UnmanagedType.LPWStr)] string appUserModelId, nint arguments, int options, out int processId);

    void ActivateForFile(nint appUserModelId, nint itemArray, nint verb, out int processId);

    void ActivateForProtocol(nint appUserModelId, nint itemArray, out int processId);
}

[ComImport, Guid("45BA127D-10A8-46EA-8AB7-56EA9078943C")]
sealed class ApplicationActivationManager : IApplicationActivationManager
{
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void ActivateApplication([MarshalAs(UnmanagedType.LPWStr)] string appUserModelId, nint arguments, int options, out int processId);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void ActivateForFile(nint appUserModelId, nint itemArray, nint verb, out int processId);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern void ActivateForProtocol(nint appUserModelId, nint itemArray, out int processId);
}