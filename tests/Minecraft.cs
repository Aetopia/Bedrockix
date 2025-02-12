using Bedrockix.Minecraft;
using System.Threading.Tasks;

static class Minecraft
{
    internal static async Task<int> LaunchAsync() => await Task.Run(Game.Launch);

    internal static async Task TerminateAsync() => await Task.Run(Game.Terminate);

    internal static async Task<bool> RunningAsync() => await Task.Run(() => Game.Running);

    internal static async Task DebugAsync(bool value) => await Task.Run(() => Game.Debug = value);
}