using System;
using System.IO;
using System.Xml;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Bedrockix.Minecraft;

sealed class Manifest
{
    static Manifest Object;

    static readonly SemaphoreSlim Semaphore = new(1, 1);

    static readonly XmlReaderSettings Settings = new() { Async = true };

    internal static Manifest Current
    {
        get
        {
            Semaphore.Wait();

            try
            {
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
        string version = default; bool instancing = default;

        while (reader.MoveToNextAttribute())
            switch (reader.LocalName)
            {
                case "Executable":
                    version = Get(@$"{package.InstalledPath}\{reader.ReadContentAsString()}");
                    break;

                case "SupportsMultipleInstances":
                    instancing = reader.ReadContentAsBoolean();
                    break;
            }

        return new(version, instancing);
    }

    internal async static Task<Manifest> CurrentAsync()
    {
        await Semaphore.WaitAsync();

        try
        {
            var properties = Properties;

            if (properties.Uncached)
                using (var reader = XmlReader.Create(properties.Path, Settings))
                    while (await reader.ReadAsync())
                        if (reader.NodeType is XmlNodeType.Element && reader.LocalName is "Application")
                        {
                            var attributes = await AttributesAsync(reader, properties.Package);
                            Object = new(properties.Path, properties.Timestamp, attributes.Version, attributes.Instancing);
                            break;
                        }

            return Object;
        }
        finally { Semaphore.Release(); }
    }

    static async Task<(string Version, bool Instancing)> AttributesAsync(XmlReader reader, Package package)
    {
        string version = default; bool instancing = default;

        while (reader.MoveToNextAttribute())
            switch (reader.LocalName)
            {
                case "Executable":
                    version = Get(@$"{package.InstalledPath}\{await reader.ReadContentAsStringAsync()}");
                    break;

                case "SupportsMultipleInstances":
                    instancing = reader.ReadContentAsBoolean();
                    break;
            }

        return new(version, instancing);
    }

    static string Get(string path)
    {
        var version = FileVersionInfo.GetVersionInfo(path).FileVersion;
        return version.Substring(default, version.LastIndexOf('.'));
    }

    static (Package Package, string Path, DateTime Timestamp, bool Uncached) Properties
    {
        get
        {
            var package = Game.App.Package;
            var path = @$"{package.InstalledPath}\AppxManifest.xml";
            var timestamp = File.GetLastWriteTimeUtc(path);
            var uncached = Object is null || Object.Timestamp != timestamp || !path.Equals(Object.Path, StringComparison.OrdinalIgnoreCase);
            return new(package, path, timestamp, uncached);
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