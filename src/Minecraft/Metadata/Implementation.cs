using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;


namespace Bedrockix.Minecraft;

public static partial class Metadata
{
    public static partial IEnumerable<Process> Processes
    {
        get
        {
            HashSet<int> collection = [];

            unsafe
            {
                fixed (char* @this = (string)Game.App)
                {
                    nint hWnd = default, hProcess = default;
                    var _ = stackalloc char[APPLICATION_USER_MODEL_ID_MAX_LENGTH];

                    while ((hWnd = FindWindowEx(hWndChildAfter: hWnd)) != default)
                        try
                        {
                            GetWindowThreadProcessId(hWnd, out var dwProcessId);
                            if (GetApplicationUserModelId(hProcess = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, dwProcessId: dwProcessId), applicationUserModelId: _)) continue;
                            else if (CompareStringOrdinal(@this, lpString2: _, bIgnoreCase: true) == CSTR_EQUAL) collection.Add(dwProcessId);
                        }
                        finally { CloseHandle(hProcess); }
                }
            }

            return collection.Select(_ => Process.GetProcessById(_));
        }
    }

    public static partial string Version => Manifest.Current.Version;

    public static partial bool Instancing => Manifest.Current.Instancing;
}
