namespace Bedrockix.Windows;

/// <summary>
/// Represents a packaged app. 
/// </summary>

public partial class App
{
    /// <summary>
    /// Check if the app is installed.
    /// </summary>

    public partial bool Installed { get; }

    /// <summary>
    /// Configure debug mode for the app.
    /// </summary>

    public partial bool Debug { set; }

    /// <summary>
    /// Check if the app is running.
    /// </summary>

    public partial bool Running { get; }

    /// <summary>
    /// Check if the app is unpackaged.
    /// </summary>

    public partial bool Unpackaged { get; }

    /// <summary>
    /// Terminate the app.
    /// </summary>

    public partial void Terminate();
}