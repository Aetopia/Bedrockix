using System;
using System.IO;
using System.Xml;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Bedrockix.Minecraft;

sealed partial class Manifest
{
    static Manifest Object;

    static readonly SemaphoreSlim Semaphore = new(1, 1);

    static readonly XmlReaderSettings Settings = new() { Async = true };

    internal static Manifest Current
    {
        get
        {
            try
            {
                Semaphore.Wait();
                var properties = Properties;

                if (properties.Uncached)
                    using (XmlReader reader = XmlReader.Create(properties.Path))
                        if (reader.ReadToFollowing("Application"))
                        {
                            var attributes = Attributes(reader, properties.Package);
                            Object = new(properties.Path, properties.Timestamp, attributes.Version, attributes.Instancing);
                        }

                return Object;
            }
            finally { Semaphore.Release(); }
        }
    }

    static (string Version, bool Instancing) Attributes(XmlReader reader, Package package)
    {
        (string Version, bool Instancing) attributes = new();

        while (reader.MoveToNextAttribute())
            switch (reader.LocalName)
            {
                case "Executable":
                    attributes.Version = Get(@$"{package.InstalledPath}\{reader.ReadContentAsString()}");
                    break;

                case "SupportsMultipleInstances":
                    attributes.Instancing = reader.ReadContentAsBoolean();
                    break;
            }

        return attributes;
    }


    internal async static Task<Manifest> CurrentAsync()
    {
        try
        {
            await Semaphore.WaitAsync().ConfigureAwait(false);
            var properties = Properties;

            if (properties.Uncached)
                using (var reader = XmlReader.Create(properties.Path, Settings))
                    while (await reader.ReadAsync().ConfigureAwait(false))
                        if (reader.NodeType is XmlNodeType.Element && reader.LocalName is "Application")
                        {
                            var attributes = await AttributesAsync(reader, properties.Package).ConfigureAwait(false);
                            Object = new(properties.Path, properties.Timestamp, attributes.Version, attributes.Instancing);
                            break;
                        }

            return Object;
        }
        finally { Semaphore.Release(); }
    }

    static async Task<(string Version, bool Instancing)> AttributesAsync(XmlReader reader, Package package)
    {
        (string Version, bool Instancing) attributes = new();

        while (reader.MoveToNextAttribute())
            switch (reader.LocalName)
            {
                case "Executable":
                    attributes.Version = Get(@$"{package.InstalledPath}\{await reader.ReadContentAsStringAsync().ConfigureAwait(false)}");
                    break;

                case "SupportsMultipleInstances":
                    attributes.Instancing = reader.ReadContentAsBoolean();
                    break;
            }

        return attributes;
    }

    static string Get(string value)
    {
        var version = FileVersionInfo.GetVersionInfo(value).FileVersion;
        return version.Substring(default, version.LastIndexOf('.'));
    }

    static (Package Package, string Path, DateTime Timestamp, bool Uncached) Properties
    {
        get
        {
            (Package Package, string Path, DateTime Timestamp, bool Uncached) properties = new() { Package = Game.App.Package };

            properties.Path = @$"{properties.Package.InstalledPath}\AppxManifest.xml";
            properties.Timestamp = File.GetLastWriteTimeUtc(properties.Path);
            properties.Uncached = Object is null || Object.Timestamp != properties.Timestamp || !properties.Path.Equals(Object.Path, StringComparison.OrdinalIgnoreCase);

            return properties;
        }
    }

    Manifest(string path, DateTime timestamp, string version, bool instancing)
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