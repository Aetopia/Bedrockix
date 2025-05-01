namespace Bedrockix.Unmanaged;

partial class Constants
{
    internal enum ACTIVATEOPTIONS { AO_NOERRORUI = 0x00000002 }

    internal enum FILE_INFO_BY_HANDLE_CLASS { FileStandardInfo = 1 }

    internal const int WAIT_TIMEOUT = 0x00000102;

    internal const int PROCESS_ALL_ACCESS = 0X1FFFFF;

    internal const int MEM_RELEASE = 0x00008000;

    internal const int MEM_COMMIT = 0x00001000;

    internal const int MEM_RESERVE = 0x00002000;

    internal const int PAGE_READWRITE = 0x40;

    internal const int DONT_RESOLVE_DLL_REFERENCES = 0x00000001;

    internal const int FILE_SHARE_DELETE = 0x00000004;

    internal const int OPEN_EXISTING = 3;

    internal const int CSTR_EQUAL = 2;
}