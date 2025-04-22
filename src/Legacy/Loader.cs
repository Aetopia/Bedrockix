using System;
using System.IO;
using System.Linq;
using System.Threading;
using Bedrockix.Windows;
using System.Security.Principal;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Runtime.InteropServices;
using static Bedrockix.Unmanaged.Native;

namespace Bedrockix.Legacy;

public static partial class Loader
{
    static readonly Core.Loader _ = Minecraft.Release.Loader;

    public static partial int? Launch(params IEnumerable<string> value) => _.Launch(value);

    public static partial int? Launch(params IReadOnlyCollection<Library> value) => _.Launch(value);
}
