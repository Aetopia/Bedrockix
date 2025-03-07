using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;
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
            using var stream = File.OpenRead(Path.Combine(path, "AppxManifest.xml"));
            var value = FileVersionInfo.GetVersionInfo(Path.Combine(path, XElement.Load(stream).Descendants().First(_ => _.Name.LocalName is "Application").Attribute("Executable").Value)).FileVersion;
            return value.Substring(default, value.LastIndexOf('.'));
        }
    }
}
