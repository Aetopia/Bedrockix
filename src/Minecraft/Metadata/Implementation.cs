using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;
using Windows.ApplicationModel;
using System.Collections.Generic;

namespace Bedrockix.Minecraft;

public static partial class Metadata
{
    static Package Package => Game.App.Package;

    static XElement Application
    {
        get
        {
            using var stream = File.OpenRead(Path.Combine(Package.InstalledPath, "AppxManifest.xml"));
            return XElement.Load(stream).Descendants().First(_ => _.Name.LocalName is "Application");
        }
    }

    public static partial bool Instancing
    {
        get
        {
            if (Package.SignatureKind is PackageSignatureKind.Store) return false;
            return Application.Attributes().Any(_ => _.Name.LocalName is "SupportsMultipleInstances" && bool.Parse(_.Value));
        }
    }

    public static partial IEnumerable<Process> Processes => Game.App.SelectMany(_ => _.GetProcessDiagnosticInfos()).Select(_ => Process.GetProcessById((int)_.ProcessId));

    public static partial string Version
    {
        get
        {
            var value = FileVersionInfo.GetVersionInfo(Path.Combine(Package.InstalledPath, Application.Attribute("Executable").Value)).FileVersion;
            return value.Substring(default, value.LastIndexOf('.'));
        }
    }
}
