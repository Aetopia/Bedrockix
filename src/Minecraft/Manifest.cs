using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Windows.ApplicationModel;

namespace Bedrockix.Minecraft;

sealed partial class Manifest
{
    static Manifest Object;

    internal static Manifest Current
    {
        get
        {
            lock (Lock)
            {
                var package = Game.App.Package;
                var path = System.IO.Path.Combine(package.InstalledPath, "AppxManifest.xml");
                var timestamp = File.GetLastWriteTimeUtc(path);

                if (Object is null || Object.Timestamp != timestamp || !path.Equals(Object.Path, StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = File.OpenRead(path);
                    var application = XElement.Load(stream).Descendants("{http://schemas.microsoft.com/appx/manifest/foundation/windows10}Application").First();

                    var version = FileVersionInfo.GetVersionInfo(System.IO.Path.Combine(package.InstalledPath, application.Attribute("Executable").Value)).FileVersion;
                    version = version.Substring(default, version.LastIndexOf('.'));

                    bool instancing = package.SignatureKind != PackageSignatureKind.Store && application.Attributes().Any(_ => _.Name.LocalName is "SupportsMultipleInstances" && bool.Parse(_.Value));

                    Object = new(path, version, timestamp, instancing);
                }

                return Object;
            }
        }
    }

    Manifest(string path, string version, DateTime timestamp, bool instancing)
    {
        Path = path;
        Version = version;
        Timestamp = timestamp;
        Instancing = instancing;
    }

    readonly string Path;

    readonly DateTime Timestamp;

    internal readonly string Version;

    internal readonly bool Instancing;
}

