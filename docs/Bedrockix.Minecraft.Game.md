# `Bedrockix.Minecraft.Game`

Provides methods to interact with Minecraft: Bedrock Edition.

- [`Game.Launch(bool)`](#gamelaunchbool)

- [`Game.Terminate()`](#gameterminate)

- [`Game.Running`](#gamerunning)

- [`Game.Installed`](#gameinstalled)

- [`Game.Debug`](#gamedebug)

- [`Game.Unpackaged`](#gameunpackaged)

## `Game.Launch(bool)`

> [!NOTE]
> - By default, this method will launch & wait for the game to initiailize.
> - The caller should specify `false` if doesn't need the library's initialization logic.

Launches Minecraft: Bedrock Edition.

- Parameter: Specify `true` to wait for the game to initialize else `false` to not.

- Returns: The process ID of the game.

## `Game.Terminate()`

Terminates Minecraft: Bedrock Edition.

## `Game.Running`

Check if Minecraft: Bedrock Edition is running.

## `Game.Installed`

Check if Minecraft: Bedrock Edition is installed.

## `Game.Debug` 

Configure debug mode for Minecraft: Bedrock Edition.

## `Game.Unpackaged`

Check if Minecraft: Bedrock Edition is unpackaged.