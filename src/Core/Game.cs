using Bedrockix.Windows;
using Windows.Management.Core;
using static Bedrockix.Unmanaged.Safe;
using Bedrockix.Unmanaged;

namespace Bedrockix.Core;

public sealed partial class Game : App
{
    internal Game(string @this, bool @params) : base(@this) { Loader = new(this); Metadata = new(this, @params); }

    public partial int? Launch(bool value)
    {
        if (!value) return base.Launch(); using var @this = Launch();
        return @this[false] ? @this.Id : null;
    }

    internal new unsafe Process Launch()
    {
        fixed (char* @this = @$"{ApplicationDataManager.CreateForPackageFamily(Package.Id.FamilyName).LocalFolder.Path}\games\com.mojang\minecraftpe\resource_init_lock")
        {
            Handle? @params = default;

            if (!Running || (@params = CreateFile(@this)) is not null)
            {
                Process @object = new(base.Launch());

                do if ((@params ??= CreateFile(@this)) is not null) break; while (@object[true]);
                using (@params) do if (GetFileInformationByHandleEx(@params)) break; while (@object[true]);

                return @object;
            }

            return new(base.Launch());
        }
    }
}