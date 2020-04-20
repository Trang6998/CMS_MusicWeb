using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface IAlbumService
    {
        IQueryable<Album> GetAlbum(string keywords);
        Task<Album> GetAlbumById(int id);
        Task CreateAlbum(Album album);
        Task UpdateAlbum(Album album);
        Task DeleteAlbum(int id);
    }
}