# `Bedrockix.Minecraft.Game`

Provides methods to interact with Minecraft: Bedrock Edition.

- [`Game.Launch(bool)`](#gamelaunchbool)

- [`Game.Terminate()`](#gameterminate)

- [`Game.Running`](#gamerunning)

- [`Game.Installed`](#gameinstalled)

- [`Game.Debug`](#gamedebug)

## `Game.Launch(bool)`

> [!NOTE]
> - By default, this method will launch & wait for the game to initiailize.
> - The caller should specify `false` if doesn't need the library's initialization logic.

> [!CAUTION]
> - This method will fail if [multi-instancing](https://learn.microsoft.com/en-us/windows/uwp/launch-resume/multi-instance-uwp) is available and the library's initialization logic is being used.
> - The caller should check if [multi-instancing is available](Bedrockix.Minecraft.Metadata.md#metadatainstancing) to determine if it should use the library's initialization logic.

Launches Minecraft: Bedrock Edition.

- Parameter: Specify `true` to wait for the game to initialize else `false` to not.

- Returns: The process ID of the game.

## `Game.Terminate()`

Terminates Minecraft: Bedrock Edition.

## `Game.Running`

Check if Minecraft: Bedrock Edition is running.

- Returns: If running then `true` else `false`.

## `Game.Installed`

Check if Minecraft: Bedrock Edition is installed.

- Returns: If installed then `true` else `false`. 

## `Game.Debug` 

Configure debug mode for Minecraft: Bedrock Edition.

- Property: Set to `true` to enable else `false` to disable.
