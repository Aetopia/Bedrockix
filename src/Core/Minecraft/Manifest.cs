using System.Threading;

namespace Bedrockix.Minecraft;

sealed partial class Manifest
{
    static readonly Lock Lock = new();
}