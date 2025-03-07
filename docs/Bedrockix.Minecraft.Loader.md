# `Bedrockix.Minecraft.Loader`

> [!CAUTION]
> - Loading arbitrary dynamic link libraries may to fail or crash the game.
> - It is the responsibility of the caller to ensure the validity of any dynamic link library.

Provides methods to load dynamic link libraries into Minecraft: Bedrock Edition.

- [`Loader.Launch(string)`<br>`Loader.Launch(Library)`](#loaderlaunchstring)

- [`Loader.Launch(params IEnumerable<string>)`<br>`Loader.Launch(params IEnumerable<Library>)`](#loaderlaunchparams-ienumerablestring)

## `Loader.Launch(string)`<br>`Loader.Launch(Library)`

Launches & loads a dynamic link library into Minecraft: Bedrock Edition.

- Parameter: The dynamic link library to load.

- Returns: The process ID of the game.

- Exceptions:

    - `FileNotFoundException`: Thrown if a specified dynamic link library doesn't exist.

    - `BadImageFormatException`: Thrown if a specified dynamic link library is invalid. 

## `Loader.Launch(params IEnumerable<string>)`<br>`Loader.Launch(params IEnumerable<Library>)`

Launches & loads dynamic link libraries into Minecraft: Bedrock Edition.

- Parameter: The dynamic link libraries to load.

- Returns: The process ID of the game.

- Exceptions:

    - `FileNotFoundException`: Thrown if any specified dynamic link library doesn't exist.

    - `BadImageFormatException`: Thrown if any specified dynamic link library is invalid. 
