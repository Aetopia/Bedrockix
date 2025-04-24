using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace Bedrockix.Core;

public sealed partial class Metadata
{
    readonly Game Game;

    readonly Manifest Manifest;

    internal Metadata(Game value) => Manifest = new(Game = value);

    public partial IEnumerable<Process> Processes => Game.Processes.Select(_ => Process.GetProcessById(_));

    public partial string Version => Manifest.Version;

    public partial bool Instancing => Manifest.Instancing;
}