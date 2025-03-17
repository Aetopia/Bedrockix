using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Bedrockix.Unmanaged;

[Guid("2E941141-7F97-4756-BA1D-9DECDE894A3D"), GeneratedComInterface(StringMarshalling = StringMarshalling.Utf16)]
partial interface IApplicationActivationManager
{
    void ActivateApplication(string appUserModelId, nint arguments, int options, out int processId);
}
