# `Bedrockix.Minecraft.Game`

Provides methods to interact with Minecraft: Bedrock Edition.

- [`Game.Launch(bool)`](#gamelaunchbool)

- [`Game.Terminate()`](#gameterminate)

- [`Game.Running`](#gamerunning)

- [`Game.Installed`](#gameinstalled)

- [`Game.Debug`](#gamedebug)

## `Game.Launch(bool)`

> [!NOTE]
> - By default, this method will launch & wait for the game to initiailized.
>
> - The caller should specify `false` if doesn't need the initialization logic provided by the library.

Launches Minecraft: Bedrock Edition.

- Parameter: Specify `true` to wait for the game to initialized else `false` to not.

- Returns: The process ID of the game.

## `Game.Terminate()`

Terminates of Minecraft: Bedrock Edition.

## `Game.Running`

Check if Minecraft: Bedrock Edition is running.

- Returns: If running then `true` else `false`.

## `Game.Installed`

Check if Minecraft: Bedrock Edition is installed.

- Returns: If installed then `true` else `false`. 

## `Game.Debug` 

Configure debug mode for Minecraft: Bedrock Edition.

- Property: Set to `true` to enable else `false` to disable.