using Bedrockix.Minecraft;
using Bedrockix.Unmanaged;

static class Program
{
    static void Main()
    {
        Game.Launch();
        string[] collection = [ @"C:\Users\User\Downloads\.mcpack"];
        var app = new App("Microsoft.MinecraftUWP_8wekyb3d8bbwe!App");
        foreach (var item in collection)
            app.File(item);
    }
}
