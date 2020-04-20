using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class AlbumService: IAlbumService
    {
        private readonly IRepository<Album> _albumRepository;
        public AlbumService(IRepository<Album> albumRepository)
        {
            _albumRepository = albumRepository;
        }
        public IQueryable<Album> GetAlbum(string keywords)
        {
            var query = _albumRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
                query = query
                    .Where(album =>
                        album.TenAlbum.Contains(keywords)
                    );
            }
            return query;
        }

        public async Task<Album> GetAlbumById(int id)
        {
            return await _albumRepository.GetByIdAsync(id);
        }
        public async Task CreateAlbum(Album album)
        {
            await _albumRepository.AddAsync(album);
        }
        public async Task UpdateAlbum(Album album)
        {
            await _albumRepository.UpdateAsync(album);
        }
        public async Task DeleteAlbum(int id)
        {
            var album = await _albumRepository.GetByIdAsync(id);
            await _albumRepository.DeleteAsync(album);
        }
    }
}