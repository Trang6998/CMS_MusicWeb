using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface IAlbum_BaiHatService
    {
        public IQueryable<Album_BaiHat> GetAlbum_BaiHat(string keywords);
        public Task<Album_BaiHat> GetAlbum_BaiHatById(int id);
        public Task CreateAlbum_BaiHat(Album_BaiHat album_BaiHat);
        public Task UpdateAlbum_BaiHat(Album_BaiHat album_BaiHat);
        public Task DeleteAlbum_BaiHat(int id);
    }
}