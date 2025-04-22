using System.Diagnostics;
using System.Collections.Generic;

namespace Bedrockix.Legacy;

public static partial class Metadata
{
    static readonly Core.Metadata _ = Minecraft.Release.Metadata;

    public static partial IEnumerable<Process> Processes => _.Processes;

    public static partial string Version => _.Version;

    public static partial bool Instancing => _.Instancing;
}
