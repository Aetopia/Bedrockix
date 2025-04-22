using System.Diagnostics;
using System.Collections.Generic;

namespace Bedrockix.Core;

/// <summary>
/// Provides metadata about Minecraft: Bedrock Edition.
/// </summary>

public partial class Metadata
{
    /// <summary>
    /// Enumerates any running processes of Minecraft: Bedrock Edition.
    /// </summary>

    public partial IEnumerable<Process> Processes { get; }

    /// <summary>
    /// Retrieves Minecraft Bedrock Edition's currently installed version.
    /// </summary>

    public partial string Version { get; }

    /// <summary>
    /// Check if multi-instancing is available for Minecraft: Bedrock Edition.
    /// </summary>

    public partial bool Instancing { get; }
}
