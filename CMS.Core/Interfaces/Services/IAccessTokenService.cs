using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface IAccessTokenService
    {
        public IQueryable<AccessToken> GetAccessToken(string keywords);
        public Task<AccessToken> GetAccessTokenById(int id);
        public Task CreateAccessToken(AccessToken accessToken);
        public Task UpdateAccessToken(AccessToken accessToken);
        public Task DeleteAccessToken(int id);
    }
}