using Bedrockix.Windows;
using Windows.Management.Core;
using static Bedrockix.Unmanaged.Safe;
using Bedrockix.Unmanaged;

namespace Bedrockix.Core;

public sealed partial class Game : App
{
    internal Game(string @this, bool @params) : base(@this) { Loader = new(this); Metadata = new(this, @params); }

    public partial int? Launch(bool value) { if (!value) return base.Launch(); using var @this = Launch(); return @this[false] ? @this.Id : null; }

    internal new unsafe Process Launch()
    {
        fixed (char* @this = @$"{ApplicationDataManager.CreateForPackageFamily(Package.Id.FamilyName).LocalFolder.Path}\games\com.mojang\minecraftpe\resource_init_lock")
        {
            Handle? @params = default;

            if (!Running || (@params = CreateFile(@this)) is not null || Metadata.Instancing)
            {
                Process @object = new(base.Launch());

                while (@params is null) if (!@object[true]) return @object; else @params = CreateFile(@this);
                using (@params) while (GetFileInformationByHandleEx((Handle)@params)) if (!@object[true]) return @object;

                return @object;
            }

            return new(base.Launch());
        }
    }
}