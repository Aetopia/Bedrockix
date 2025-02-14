# Bedrock.Minecraft.Game

Provides methods to interact with Minecraft: Bedrock Edition.

- [`Game.Launch()`](#gamelaunch)
- [`Game.Terminate()`](#gameterminate)
- [`Game.Running`](#gamerunning)
- [`Game.Debug`](#gamedebug)

## `Game.Launch()`

Launches an instance of Minecraft.

- Returns: 
    
    The process ID of the instance.

- Exceptions:
    
    - `OperationCanceledException`: 
        
        Thrown if the instance terminates prematurely.

## `Game.Terminate()`

Terminate any running instances of Minecraft: Bedrock Edition.

## `Game.Running`

Check for any running instance of Minecraft: Bedrock Edition.

- Returns: 
    
    If a running instance is present then `true` else `false`.

## `Game.Debug` 

Configure debug mode for Minecraft: Bedrock Edition.

- Property: 

    Set to `true` to enable debug mode, `false` to disable.