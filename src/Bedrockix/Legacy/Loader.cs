using System;
using System.IO;
using Bedrockix.Windows;
using System.Collections.Generic;

namespace Bedrockix.Legacy;

/// <summary>
/// Provides methods to load dynamic link libraries into Minecraft: Bedrock Edition.
/// </summary>

public static partial class Loader
{
    /// <summary>
    /// Launches &amp; loads dynamic link libraries into Minecraft: Bedrock Edition.
    /// </summary>

    /// <param name="value">
    /// The dynamic link libraries to load.
    /// </param>

    /// <returns>
    /// The process ID of the game.
    /// </returns>

    /// <exception cref="FileNotFoundException">
    /// Thrown if any specified dynamic link library doesn't exist.
    /// </exception>

    /// <exception cref="BadImageFormatException">
    /// Thrown if any specified dynamic link library is invalid. 
    /// </exception>

    public static partial int? Launch(params IReadOnlyCollection<Library> value);

    /// <summary>
    /// Launches &amp; loads dynamic link libraries into Minecraft: Bedrock Edition.
    /// </summary>

    /// <param name="value">
    /// The dynamic link libraries to load.
    /// </param>

    /// <returns>
    /// The process ID of the game.
    /// </returns>

    /// <exception cref="FileNotFoundException">
    /// Thrown if any specified dynamic link library doesn't exist.
    /// </exception>

    /// <exception cref="BadImageFormatException">
    /// Thrown if any specified dynamic link library is invalid. 
    /// </exception>

    public static partial int? Launch(params IEnumerable<string> value);
}