namespace Bedrockix.Unmanaged;

static class Constants
{
    internal const int WAIT_TIMEOUT = 0x00000102;

    internal const int AO_NOERRORUI = 0x00000002;

    internal const int PROCESS_ALL_ACCESS = 0X1FFFFF;

    internal const int MEM_RELEASE = 0x00008000;

    internal const int MEM_COMMIT = 0x00001000;

    internal const int MEM_RESERVE = 0x00002000;

    internal const int PAGE_READWRITE = 0x40;

    internal const int DONT_RESOLVE_DLL_REFERENCES = 0x00000001;

    internal const int FILE_SHARE_DELETE = 0x00000004;

    internal const nint INVALID_HANDLE_VALUE = -1;

    internal const int FileStandardInfo = 1;

    internal const int OPEN_EXISTING = 3;

    internal const int PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;

    internal const int APPLICATION_USER_MODEL_ID_MAX_LENGTH = 130;

    internal const int CSTR_EQUAL = 2;

    internal const int PACKAGE_RELATIVE_APPLICATION_ID_MAX_LENGTH = 65;
}