using System;
using System.Diagnostics;
using System.IO;
using System.Xml;

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
                    string version = default; bool instancing = default;
                    using XmlReader reader = XmlReader.Create(path);

                    if (reader.ReadToFollowing("Application"))
                        while (reader.MoveToNextAttribute())
                            switch (reader.LocalName)
                            {
                                case "Executable":
                                    version = FileVersionInfo.GetVersionInfo(System.IO.Path.Combine(package.InstalledPath, reader.ReadContentAsString())).FileVersion;
                                    break;

                                case "SupportsMultipleInstances":
                                    if (!instancing) instancing = reader.ReadContentAsBoolean();
                                    break;
                            }

                    Object = new(path, version.Substring(default, version.LastIndexOf('.')), timestamp, instancing);
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