using System.Linq;
using Windows.System;
using System.Threading;
using System.Diagnostics;
using Windows.Foundation;
using System.Collections.Generic;

namespace Bedrockix.Minecraft;

public static partial class Metadata
{
    public static partial IEnumerable<Process> Processes
    {
        get
        {
            var @this = AppDiagnosticInfo.RequestInfoForAppAsync(Game.App);
            try
            {
                if (@this.Status is AsyncStatus.Started)
                {
                    using ManualResetEventSlim @event = new();
                    @this.Completed += (_, _) => @event.Set();
                    @event.Wait();
                }

                if (@this.Status is AsyncStatus.Error)
                    throw @this.ErrorCode;

                return @this.GetResults()[default].GetResourceGroups().SelectMany(_ => _.GetProcessDiagnosticInfos().Select(_ => Process.GetProcessById((int)_.ProcessId)));
            }
            finally { @this.Close(); }
        }
    }

    public static partial string Version => Manifest.Current.Version;

    public static partial bool Instancing => Manifest.Current.Instancing;
}
