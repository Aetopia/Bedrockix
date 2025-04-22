namespace Bedrockix.Core;

/// <summary>
/// Provides methods to interact with Minecraft: Bedrock Edition.
/// </summary>

public partial class Game
{
    /// <summary>
    /// Provides methods to load dynamic link libraries into Minecraft: Bedrock Edition.
    /// </summary>

    public readonly Loader Loader;

    /// <summary>
    /// Provides metadata about Minecraft: Bedrock Edition.
    /// </summary>

    public readonly Metadata Metadata;

    /// <summary>
    /// Check if Minecraft: Bedrock Edition is unpackaged.
    /// </summary>

    public partial bool Unpackaged { get; }

    /// <summary>
    /// Launches Minecraft: Bedrock Edition.
    /// </summary>

    /// <param name="value">
    /// Specify <c>true</c> to wait for the game to initialize else <c>false</c> to not.
    /// </param>

    /// <returns>
    /// The process ID of the game.
    /// </returns>

    public partial int? Launch(bool value = true);
}