namespace Bedrockix;

/// <summary>
/// Provides access to Minecraft &amp; Minecraft: Preview.
/// </summary>

public static class Minecraft
{
    /// <summary>
    /// Provides access to Minecraft.
    /// </summary>

    public static readonly Core.Game Release = new("Microsoft.MinecraftUWP_8wekyb3d8bbwe!App", false);

    /// <summary>
    /// Provides access to Minecraft Preview.
    /// </summary>

    public static readonly Core.Game Preview = new("Microsoft.MinecraftWindowsBeta_8wekyb3d8bbwe!App", true);
}
