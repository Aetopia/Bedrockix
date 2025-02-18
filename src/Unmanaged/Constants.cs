namespace Bedrockix.Unmanaged;

static class Constants
{
    internal const int AO_NOERRORUI = 0x00000002;

    internal const int SYNCHRONIZE = unchecked((int)0x00100000L);

    internal const int LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800;

    internal const int MEM_RELEASE = 0x00008000;

    internal const int PROCESS_ALL_ACCESS = 0X1FFFFF;

    internal const int MEM_COMMIT = 0x00001000;

    internal const int MEM_RESERVE = 0x00002000;

    internal const int PAGE_EXECUTE_READWRITE = 0x40;

    internal const int ERROR_INSUFFICIENT_BUFFER = 0x7A;
}