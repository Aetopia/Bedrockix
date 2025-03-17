namespace Bedrockix.Unmanaged;

static class Constants
{
    internal const int AO_NOERRORUI = 0x00000002;

    internal const int PROCESS_ALL_ACCESS = 0X1FFFFF;

    internal const int MEM_RELEASE = 0x00008000;

    internal const int MEM_COMMIT = 0x00001000;

    internal const int MEM_RESERVE = 0x00002000;

    internal const int PAGE_READWRITE = 0x40;

    internal const int ERROR_INSUFFICIENT_BUFFER = 0x7A;

    internal const int DONT_RESOLVE_DLL_REFERENCES = 0x00000001;

    internal const int STATUS_PENDING = 0x00000103;

    internal const int INVALID_FILE_ATTRIBUTES = -1;

    internal const int FILE_ATTRIBUTE_DIRECTORY = 0x00000010;

    internal const int CLSCTX_INPROC_SERVER = 0x1;
}