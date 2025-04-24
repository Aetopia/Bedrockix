using System;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace Bedrockix.Core;

sealed class Manifest
{
    readonly object _ = new();

    readonly Game Game;

    DateTime Timestamp;

    string Path;

    internal Manifest(Game value) => Game = value;

    internal string Version { get { Get(); return field; } private set; }

    internal bool Instancing { get { Get(); return field; } private set; }

    static readonly XmlReaderSettings Settings = new() { IgnoreComments = true, IgnoreWhitespace = true, IgnoreProcessingInstructions = true };

    void Get()
    {
        lock (_)
        {
            var package = Game.Package;
            var path = @$"{package.InstalledPath}\AppxManifest.xml";
            var timestamp = File.GetLastWriteTimeUtc(path);

            if (Timestamp != timestamp || !path.Equals(Path, StringComparison.OrdinalIgnoreCase))
            {
                (Timestamp, Path) = (timestamp, path);
                using var reader = XmlReader.Create(path, Settings);

                if (reader.ReadToFollowing("Application"))
                    while (reader.MoveToNextAttribute())
                        switch (reader.LocalName)
                        {
                            case "Executable":
                                var _ = FileVersionInfo.GetVersionInfo(@$"{package.InstalledPath}\{reader.ReadContentAsString()}").FileVersion;
                                Version = _.Substring(new(), _.LastIndexOf('.'));
                                break;

                            case "SupportsMultipleInstances":
                                Instancing = reader.ReadContentAsBoolean();
                                break;
                        }
            }
        }
    }
}