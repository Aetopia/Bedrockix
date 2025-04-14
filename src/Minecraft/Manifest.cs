using System;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace Bedrockix.Minecraft;

sealed partial class Manifest
{
    static readonly object _ = new();

    static Manifest Value;

    static readonly XmlReaderSettings Settings = new()
    {
        IgnoreComments = true,
        IgnoreWhitespace = true,
        IgnoreProcessingInstructions = true
    };

    internal readonly string Path, Version;

    internal readonly DateTime Timestamp;

    internal readonly bool Instancing;

    Manifest(string path, string version, DateTime timestamp, bool instancing)
    {
        Path = path;
        Version = version;
        Timestamp = timestamp;
        Instancing = instancing;
    }

    internal static Manifest Current
    {
        get
        {
            lock (_)
            {
                var package = Game.App.Package;
                var path = @$"{package.InstalledPath}\AppxManifest.xml";
                var timestamp = File.GetLastWriteTimeUtc(path);

                if (Value is null || Value.Timestamp != timestamp || !Value.Path.Equals(path, StringComparison.OrdinalIgnoreCase))
                {
                    string version = default;
                    bool instancing = default;
                    using var reader = XmlReader.Create(path, Settings);

                    if (reader.ReadToFollowing("Application"))
                        while (reader.MoveToNextAttribute())
                            switch (reader.LocalName)
                            {
                                case "Executable":
                                    version = FileVersionInfo.GetVersionInfo(@$"{package.InstalledPath}\{reader.ReadContentAsString()}").FileVersion;
                                    break;

                                case "SupportsMultipleInstances":
                                    instancing = reader.ReadContentAsBoolean();
                                    break;
                            }

                    Value = new(path, version.Substring(default, version.LastIndexOf('.')), timestamp, instancing);
                }

                return Value;
            }
        }
    }
}