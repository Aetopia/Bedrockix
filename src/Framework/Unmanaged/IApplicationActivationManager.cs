using System.Runtime.InteropServices;

namespace Bedrockix.Unmanaged;

[Guid("2E941141-7F97-4756-BA1D-9DECDE894A3D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IApplicationActivationManager
{
    void ActivateApplication([MarshalAs(UnmanagedType.LPWStr)] string appUserModelId, nint arguments, int options, out int processId);
}
