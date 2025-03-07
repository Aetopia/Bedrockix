using System;
using System.IO;
using System.Collections.Generic;
using Bedrockix.Windows;

namespace Bedrockix.Minecraft;

/// <summary>
/// Provides methods to load dynamic link libraries into Minecraft: Bedrock Edition.
/// </summary>

public static partial class Loader
{
    /// <summary>
    /// Launches &amp; loads a dynamic link library into Minecraft: Bedrock Edition.
    /// </summary>

    /// <param name="library">
    /// The dynamic link library to load.
    /// </param>

    /// <returns>
    /// The process ID of the game.
    /// </returns>

    /// <exception cref="FileNotFoundException">
    /// Thrown if a specified dynamic link library doesn't exist.
    /// </exception>

    /// <exception cref="BadImageFormatException">
    /// Thrown if a specified dynamic link library is invalid. 
    /// </exception>

    public static partial int? Launch(Library library);

    /// <summary>
    /// Launches &amp; loads dynamic link libraries into Minecraft: Bedrock Edition.
    /// </summary>

    /// <param name="libraries">
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

    public static partial int? Launch(params IEnumerable<Library> libraries);

    /// <summary>
    /// Launches &amp; loads a dynamic link library into Minecraft: Bedrock Edition.
    /// </summary>

    /// <param name="path">
    /// The dynamic link library to load.
    /// </param>

    /// <returns>
    /// The process ID of the game.
    /// </returns>

    /// <exception cref="FileNotFoundException">
    /// Thrown if a specified dynamic link library doesn't exist.
    /// </exception>

    /// <exception cref="BadImageFormatException">
    /// Thrown if a specified dynamic link library is invalid. 
    /// </exception>

    public static partial int? Launch(string path);

    /// <summary>
    /// Launches &amp; loads dynamic link libraries into Minecraft: Bedrock Edition.
    /// </summary>

    /// <param name="paths">
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

    public static partial int? Launch(params IEnumerable<string> paths);
}