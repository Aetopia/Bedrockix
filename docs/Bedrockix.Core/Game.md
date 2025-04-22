# `Game`

## `Game.Loader`

Provides methods to load dynamic link libraries into Minecraft: Bedrock Edition.

## `Game.Metadata` 

Provides metadata about Minecraft: Bedrock Edition.

## `Game.Launch(bool)`

> [!NOTE]
> - By default, this method will launch & wait for the game to initiailize.
> - The caller should specify `false` if doesn't need the library's initialization logic.

Launches Minecraft: Bedrock Edition.

- Parameter: Specify `true` to wait for the game to initialize else `false` to not.

- Returns: The process ID of the game.