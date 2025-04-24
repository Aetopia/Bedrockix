using Bedrockix.Windows;
using Windows.Management.Core;
using Bedrockix.Unmanaged.Structures;
using static Bedrockix.Unmanaged.Native;
using static Bedrockix.Unmanaged.Constants;

namespace Bedrockix.Core;

public sealed partial class Game : App
{
    internal Game(string value) : base(value) => (Loader, Metadata) = (new(this), new(this));

    public partial int? Launch(bool value)
    {
        if (!value) return base.Launch();
        using var @this = Launch();
        return @this[false] ? @this.Id : null;
    }

    internal new unsafe Instance Launch()
    {
        fixed (char* lpFileName = @$"{ApplicationDataManager.CreateForPackageFamily(Package.Id.FamilyName).LocalFolder.Path}\games\com.mojang\minecraftpe\resource_init_lock")
        {
            var hFile = INVALID_HANDLE_VALUE;

            try
            {
                if (!Running || (hFile = CreateFile2(lpFileName)) != INVALID_HANDLE_VALUE || Metadata.Instancing)
                {
                    Instance @this = new(base.Launch());

                    while (hFile is INVALID_HANDLE_VALUE)
                        if (!@this[true]) return @this;
                        else hFile = CreateFile2(lpFileName);

                    while (GetFileInformationByHandleEx(hFile, FileStandardInfo, out var lpFileInformation, sizeof(FILE_STANDARD_INFO)) && !lpFileInformation.DeletePending)
                        if (!@this[true]) return @this;

                    return @this;
                }
            }
            finally { CloseHandle(hFile); }

            return new(base.Launch());
        }
    }
}