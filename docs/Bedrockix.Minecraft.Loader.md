# `Bedrockix.Minecraft.Loader`

> [!CAUTION]
> - Loading arbitrary dynamic link libraries may to fail or crash the game.
> - It is the responsibility of the caller to ensure the validity of any dynamic link library.

Provides methods to load dynamic link libraries into Minecraft: Bedrock Edition.

- [`Loader.Launch(string)`](#loaderlaunchstring)

- [`Loader.Launch(params IEnumerable<string>)`](#loaderlaunchparams-ienumerablestring)

## `Loader.Launch(string)`

Launches & loads a dynamic link library into Minecraft: Bedrock Edition.

- Parameter: The dynamic link library to load.

- Returns: The process ID of the game.

## `Loader.Launch(params IEnumerable<string>)`

Launches & loads a dynamic link libraries into Minecraft: Bedrock Edition.

- Parameter: The dynamic link libraries to load.

- Returns: The process ID of the game.
