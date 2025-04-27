using System.Diagnostics;
using System.Collections.Generic;

namespace Bedrockix.Core;

public sealed partial class Metadata
{
    readonly Game Game;

    readonly Manifest Manifest;

    internal Metadata(Game @this) { Game = @this; Manifest = new(@this); }

    public partial IEnumerable<Process> Processes { get { HashSet<int> @this = []; foreach (var _ in Game.Processes) if (@this.Add(_)) yield return Process.GetProcessById(_); } }

    public partial string Version => Manifest.Version;

    public partial bool Instancing => Manifest.Instancing;
}