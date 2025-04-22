using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Core;

public sealed partial class Metadata
{
    readonly Game Game;

    readonly Manifest Manifest;

    internal Metadata(Game value) => Manifest = new(Game = value);

    public partial IEnumerable<Process> Processes
    {
        get
        {
            HashSet<int> collection = [];

            unsafe
            {
                fixed (char* @this = Game.Id)
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

    public partial string Version => Manifest.Version;

    public partial bool Instancing => Manifest.Instancing;
}