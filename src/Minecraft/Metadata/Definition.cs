using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bedrockix.Minecraft;

/// <summary>
/// Provides metadata about Minecraft: Bedrock Edition.
/// </summary>

public static partial class Metadata
{
    /// <summary>
    /// Enumerates any running processes of Minecraft: Bedrock Edition.
    /// </summary>

    public static partial IEnumerable<Process> Processes { get; }

    /// <summary>
    /// Retrieves Minecraft Bedrock Edition's currently installed version.
    /// </summary>

    public static partial string Version { get; }

    /// <summary>
    /// Check if multi-instancing is available for Minecraft: Bedrock Edition.
    /// </summary>

    public static partial bool Instancing { get; }
}
