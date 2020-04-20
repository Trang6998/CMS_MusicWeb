using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface IAlbum_BaiHatService
    {
        IQueryable<Album_BaiHat> GetAlbum_BaiHat(string keywords);
        Album_BaiHat GetAlbum_BaiHatById(int id);
        Album_BaiHat CreateAlbum_BaiHat(Album_BaiHat album_BaiHat);
        Album_BaiHat UpdateAlbum_BaiHat(Album_BaiHat album_BaiHat);
        Album_BaiHat DeleteAlbum_BaiHat(int id);
    }
}