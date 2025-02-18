using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Bedrockix.Minecraft;

/// <summary>
/// Provides metadata about Minecraft: Bedrock Edition.
/// </summary>
public static class Metadata
{
    /// <summary>
    /// Enumerates any running processes of Minecraft: Bedrock Edition.
    /// </summary>
    public static IEnumerable<Process> Processes => Game.App.Value.Info.GetResourceGroups().SelectMany(_ => _.GetProcessDiagnosticInfos()).Select(_ => Process.GetProcessById((int)_.ProcessId));

    /// <summary>
    /// Retrieves Minecraft Bedrock Edition's currently installed version.
    /// </summary>
    public static string Version
    {
        get
        {
            var path = Game.App.Value.Info.AppInfo.Package.InstalledPath;
            using var stream = File.OpenRead(Path.Combine(path, "AppxManifest.xml"));
            var value = FileVersionInfo.GetVersionInfo(Path.Combine(path, XElement.Load(stream).Descendants().First(_ => _.Name.LocalName is "Application").Attribute("Executable").Value)).FileVersion;
            return value[..value.LastIndexOf('.')];
        }
    }
}
