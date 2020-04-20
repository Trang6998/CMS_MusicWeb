using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface IAlbumService
    {
        public IQueryable<Album> GetAlbum(string keywords);
        public Task<Album> GetAlbumById(int id);
        public Task CreateAlbum(Album album);
        public Task UpdateAlbum(Album album);
        public Task DeleteAlbum(int id);
    }
}