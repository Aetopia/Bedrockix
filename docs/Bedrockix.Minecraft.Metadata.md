# `Bedrockix.Minecraft.Metadata`

Provides metadata about Minecraft: Bedrock Edition.

- [`Metadata.Processes`](#metadataprocesses)

- [`Metadata.Version`](#metadataversion)

- [`Metadata.Instancing`](#metadatainstancing)

- [`Metadata.VersionAsync()`](#metadataversionasync)

- [`Metadata.InstancingAsync()`](#metadatainstancingasync)

## `Metadata.Processes`

> [!NOTE]
> - Users may modify the game's package manifest to support [multi-instancing](https://learn.microsoft.com/en-us/windows/uwp/launch-resume/multi-instance-uwp).
> - Use [`Game.Running`](Bedrockix.Minecraft.Game.md#gamerunning) to check if the game is running.

Enumerates any running processes of Minecraft: Bedrock Edition.

## `Metadata.Version`

Retrieves Minecraft Bedrock Edition's currently installed version.

## `Metadata.Instancing`

Check if multi-instancing is available for Minecraft: Bedrock Edition.

## `Metadata.VersionAsync()`

Asynchronously retrieves Minecraft Bedrock Edition's currently installed version.
    
## `Metadata.InstancingAsync()`

Asynchronously check if multi-instancing is available for Minecraft: Bedrock Edition.