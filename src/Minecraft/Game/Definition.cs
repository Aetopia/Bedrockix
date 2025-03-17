namespace Bedrockix.Minecraft;

/// <summary>
/// Provides methods to interact with Minecraft: Bedrock Edition.
/// </summary>

public static partial class Game
{
    /// <summary>
    /// Launches Minecraft: Bedrock Edition.
    /// </summary>

    /// <param name="value">
    /// Specify <c>true</c> to wait for the game to initialize else <c>false</c> to not.
    /// </param>

    /// <returns>
    /// The process ID of the game.
    /// </returns>

    public static partial int? Launch(bool value = true);

    /// <summary>
    /// Terminates Minecraft: Bedrock Edition.
    /// </summary>

    public static partial void Terminate();

    /// <summary>
    /// Check if Minecraft: Bedrock Edition is installed.
    /// </summary>

    public static partial bool Installed { get; }

    /// <summary>
    /// Check if Minecraft: Bedrock Edition is running.
    /// </summary>

    public static partial bool Running { get; }

    /// <summary>
    /// Configure debug mode for Minecraft: Bedrock Edition.
    /// </summary>

    public static partial bool Debug { set; }
}