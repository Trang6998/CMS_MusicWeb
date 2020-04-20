using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class AccessTokenService: IAccessTokenService
    {
        private readonly IRepository<AccessToken> _accessTokenRepository;
        public AccessTokenService(IRepository<AccessToken> accessTokenRepository)
        {
            _accessTokenRepository = accessTokenRepository;
        }
        public IQueryable<AccessToken> GetAccessToken(string keywords)
        {
            var query = _accessTokenRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
              
            }
            return query;
        }

        public async Task<AccessToken> GetAccessTokenById(int id)
        {
            return await _accessTokenRepository.GetByIdAsync(id);
        }
        public async Task CreateAccessToken(AccessToken accessToken)
        {
            await _accessTokenRepository.AddAsync(accessToken);
        }
        public async Task UpdateAccessToken(AccessToken accessToken)
        {
            await _accessTokenRepository.UpdateAsync(accessToken);
        }
        public async Task DeleteAccessToken(int id)
        {
            var accessToken = await _accessTokenRepository.GetByIdAsync(id);
            await _accessTokenRepository.DeleteAsync(accessToken);
        }
    }
}