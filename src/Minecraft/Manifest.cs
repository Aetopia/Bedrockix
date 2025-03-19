using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Bedrockix.Minecraft;

static class Manifest
{
    internal static XElement Application(string value)
    {
        using var stream = File.OpenRead(Path.Combine(value, "AppxManifest.xml"));
        return XElement.Load(stream).Descendants().First(_ => _.Name.LocalName is "Application");
    }
}