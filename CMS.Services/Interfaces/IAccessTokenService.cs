using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface IAccessTokenService
    {
        IQueryable<AccessToken> GetAccessToken(string keywords);
        Task<AccessToken> GetAccessTokenById(int id);
        Task CreateAccessToken(AccessToken accessToken);
        Task UpdateAccessToken(AccessToken accessToken);
        Task DeleteAccessToken(int id);
    }
}