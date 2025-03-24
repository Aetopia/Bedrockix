using System;
using System.Threading;
using System.Runtime.CompilerServices;

readonly struct Context : INotifyCompletion
{
    public void OnCompleted(Action continuation)
    {
        var context = SynchronizationContext.Current;
        try { SynchronizationContext.SetSynchronizationContext(default); continuation(); }
        finally { SynchronizationContext.SetSynchronizationContext(context); }
    }

    public bool IsCompleted => SynchronizationContext.Current is null;

    public Context GetAwaiter() => this;

    public void GetResult() { }
}