using Bedrockix.Windows;
using Windows.Management.Core;
using Bedrockix.Unmanaged.Structures;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Core;

public sealed partial class Game : App
{
    internal Game(string @this) : base(@this) => (Loader, Metadata) = (new(this), new(this));

    public partial int? Launch(bool value)
    {
        if (!value) return base.Launch();
        using var @this = Launch();
        return @this[false] ? @this.Id : null;
    }

    internal new unsafe Process Launch()
    {
        fixed (char* lpFileName = @$"{ApplicationDataManager.CreateForPackageFamily(Package.Id.FamilyName).LocalFolder.Path}\games\com.mojang\minecraftpe\resource_init_lock")
        {
            var @params = INVALID_HANDLE_VALUE;

            try
            {
                if (!Running || (@params = CreateFile2(lpFileName)) != INVALID_HANDLE_VALUE || Metadata.Instancing)
                {
                    Process @this = new(base.Launch());

                    while (@params is INVALID_HANDLE_VALUE)
                        if (!@this[true]) return @this;
                        else @params = CreateFile2(lpFileName);

                    while (GetFileInformationByHandleEx(@params, FileStandardInfo, out var @object, sizeof(FILE_STANDARD_INFO)) && !@object.DeletePending)
                        if (!@this[true]) return @this;

                    return @this;
                }
            }
            finally { CloseHandle(@params); }

            return new(base.Launch());
        }
    }
}