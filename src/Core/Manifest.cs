using System;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace Bedrockix.Core;

sealed class Manifest(Game @this, bool @params)
{
    readonly object @object = new(); DateTime Timestamp; string Path;

    internal string Version { get { Get(); return field; } private set; }

    internal bool Instancing { get { Get(); return field; } private set; }

    static readonly XmlReaderSettings Settings = new() { IgnoreComments = true, IgnoreWhitespace = true, IgnoreProcessingInstructions = true };

    void Get()
    {
        lock (@object)
        {
            var package = @this.Package;
            var path = @$"{package.InstalledPath}\AppxManifest.xml";
            var timestamp = File.GetLastWriteTimeUtc(path);

            if (Timestamp != timestamp || !path.Equals(Path, StringComparison.OrdinalIgnoreCase))
            {
                Timestamp = timestamp; Path = path;
                using var reader = XmlReader.Create(path, Settings);

                if (reader.ReadToFollowing("Application"))
                    while (reader.MoveToNextAttribute())
                        switch (reader.LocalName)
                        {
                            case "Executable":
                                var @string = FileVersionInfo.GetVersionInfo(@$"{package.InstalledPath}\{reader.ReadContentAsString()}").FileVersion;
                                Version = @params ? @string : @string .Substring(new(), @string .LastIndexOf('.'));
                                break;

                            case "SupportsMultipleInstances":
                                Instancing = reader.ReadContentAsBoolean();
                                break;
                        }
            }
        }
    }
}