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
            CreateFile(@this, out var @params); if (!Running || @params)
            {
                Process @object = new(base.Launch());

                while (!@params)
                {
                    if (!@object[true]) return @object;
                    CreateFile(@this, out @params);
                }

                using (@params) while (@object[true])
                    {
                        GetFileInformationByHandleEx(@params, out var @struct);
                        if (@struct.DeletePending) break;
                    }

                return @object;
            }

            return new(base.Launch());
        }
    }
}