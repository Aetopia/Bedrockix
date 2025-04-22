namespace Bedrockix.Legacy;

public static partial class Game
{
    static readonly Core.Game _ = Minecraft.Release;

    public static partial int? Launch(bool value) => _.Launch(value);

    public static partial void Terminate() => _.Terminate();

    public static partial bool Installed => _.Installed;

    public static partial bool Running => _.Running;

    public static partial bool Debug { set => _.Debug = value; }

    public static partial bool Unpackaged => _.Package.IsDevelopmentMode;
}