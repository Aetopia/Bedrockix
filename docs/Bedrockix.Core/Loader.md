# `Loader`

> [!CAUTION]
> - Loading arbitrary dynamic link libraries may fail or crash the game.
> - It is the responsibility of the caller to ensure the validity of any dynamic link library.

Provides methods to load dynamic link libraries into Minecraft: Bedrock Edition.

## `Loader.Launch(params IEnumerable<string>)`<br>`Loader.Launch(params IReadOnlyCollection<Library>)`

> [!IMPORTANT]
> This method uses the library's initialization logic when calling [`Game.Launch()`](Game.md#gamelaunchbool).

> [!TIP]
> - Use the [`Library`](../Bedrockix.Windows/Library.md) class to validate dynamic link libraries at runtime.
> - This allows the caller to ensure that the passed dynamic link libraries are valid.

Launches & loads dynamic link libraries into Minecraft: Bedrock Edition.

- Parameter: The dynamic link libraries to load.

- Returns: The process ID of the game.

- Exceptions:

    - `FileNotFoundException`: Thrown if any specified dynamic link library doesn't exist.

    - `BadImageFormatException`: Thrown if any specified dynamic link library is invalid. 
