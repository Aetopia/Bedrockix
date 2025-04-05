using System.Diagnostics;
using System.Collections.Generic;

namespace Bedrockix.Minecraft;

public static partial class Metadata
{
    public static partial IEnumerable<Process> Processes => Game.App.Processes;

    public static partial string Version => Manifest.Current.Version;

    public static partial bool Instancing => Manifest.Current.Instancing;
}
