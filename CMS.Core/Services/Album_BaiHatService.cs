using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class Album_BaiHatService: IAlbum_BaiHatService
    {
        private readonly IRepository<Album_BaiHat> _album_BaiHatRepository;
        public Album_BaiHatService(IRepository<Album_BaiHat> album_BaiHatRepository)
        {
            _album_BaiHatRepository = album_BaiHatRepository;
        }
        public IQueryable<Album_BaiHat> GetAlbum_BaiHat(string keywords)
        {
            var query = _album_BaiHatRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
                
            }
            return query;
        }

        public async Task<Album_BaiHat> GetAlbum_BaiHatById(int id)
        {
            return await _album_BaiHatRepository.GetByIdAsync(id);
        }
        public async Task CreateAlbum_BaiHat(Album_BaiHat album_BaiHat)
        {
            await _album_BaiHatRepository.AddAsync(album_BaiHat);
        }
        public async Task UpdateAlbum_BaiHat(Album_BaiHat album_BaiHat)
        {
            await _album_BaiHatRepository.UpdateAsync(album_BaiHat);
        }
        public async Task DeleteAlbum_BaiHat(int id)
        {
            var album_BaiHat = await _album_BaiHatRepository.GetByIdAsync(id);
            await _album_BaiHatRepository.DeleteAsync(album_BaiHat);
        }
    }
}