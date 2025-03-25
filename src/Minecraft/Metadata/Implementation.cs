using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace Bedrockix.Minecraft;

public static partial class Metadata
{
    public static partial IEnumerable<Process> Processes => Game.App.SelectMany(_ => _.GetProcessDiagnosticInfos()).Select(_ => Process.GetProcessById((int)_.ProcessId));

    public static partial string Version => Manifest.Current.Version;

    public static partial bool Instancing => Manifest.Current.Instancing;
}
