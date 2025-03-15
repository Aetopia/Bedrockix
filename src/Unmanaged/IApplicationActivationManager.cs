using System.Runtime.InteropServices;

namespace Bedrockix.Unmanaged;

[ComImport, Guid("45BA127D-10A8-46EA-8AB7-56EA9078943C")]
class ApplicationActivationManager;

[Guid("2E941141-7F97-4756-BA1D-9DECDE894A3D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IApplicationActivationManager
{
    void ActivateApplication([MarshalAs(UnmanagedType.LPWStr)] string appUserModelId, nint arguments, int options, out int processId);

    void ActivateForFile(nint appUserModelId, nint itemArray, nint verb, out int processId);

    void ActivateForProtocol(nint appUserModelId, nint itemArray, out int processId);
}

