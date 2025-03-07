namespace Bedrockix.Windows;

/// <summary>
/// Provides properties to validate dynamic link libraries.
/// </summary>

public sealed partial class Library
{
    /// <summary>
    /// Check if the dynamic link library is valid.
    /// </summary>

    public readonly bool Valid;

    /// <summary>
    /// Check if the dynamic link library exists.
    /// </summary>

    public readonly bool Exists;

    /// <summary>
    /// Path of the dynamic link library.
    /// </summary>

    public readonly string Path;
}