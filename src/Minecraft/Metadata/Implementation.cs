using System.IO;
using System.Linq;
using System.Diagnostics;
using Windows.ApplicationModel;
using System.Collections.Generic;

namespace Bedrockix.Minecraft;

public static partial class Metadata
{
    public static partial IEnumerable<Process> Processes => Game.App.SelectMany(_ => _.GetProcessDiagnosticInfos()).Select(_ => Process.GetProcessById((int)_.ProcessId));

    public static partial string Version
    {
        get
        {
            var path = Game.App.Package.InstalledPath;
            var value = FileVersionInfo.GetVersionInfo(Path.Combine(path, Manifest.Application(path).Attribute("Executable").Value)).FileVersion;
            return value.Substring(default, value.LastIndexOf('.'));
        }
    }

    public static partial bool Instancing
    {
        get
        {
            var package = Game.App.Package;
            if (package.SignatureKind is PackageSignatureKind.Store) return default;
            return Manifest.Application(package.InstalledPath).Attributes().Any(_ => _.Name.LocalName is "SupportsMultipleInstances" && bool.Parse(_.Value));
        }
    }
}
