using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class CaSy_BaiHatService: ICaSy_BaiHatService
    {
        private readonly IRepository<CaSy_BaiHat> _caSy_BaiHatRepository;
        public CaSy_BaiHatService(IRepository<CaSy_BaiHat> caSy_BaiHatRepository)
        {
            _caSy_BaiHatRepository = caSy_BaiHatRepository;
        }
        public IQueryable<CaSy_BaiHat> GetCaSy_BaiHat(string keywords)
        {
            var query = _caSy_BaiHatRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
                
            }
            return query;
        }

        public async Task<CaSy_BaiHat> GetCaSy_BaiHatById(int id)
        {
            return await _caSy_BaiHatRepository.GetByIdAsync(id);
        }
        public async Task CreateCaSy_BaiHat(CaSy_BaiHat caSy_BaiHat)
        {
            await _caSy_BaiHatRepository.AddAsync(caSy_BaiHat);
        }
        public async Task UpdateCaSy_BaiHat(CaSy_BaiHat caSy_BaiHat)
        {
            await _caSy_BaiHatRepository.UpdateAsync(caSy_BaiHat);
        }
        public async Task DeleteCaSy_BaiHat(int id)
        {
            var caSy_BaiHat = await _caSy_BaiHatRepository.GetByIdAsync(id);
            await _caSy_BaiHatRepository.DeleteAsync(caSy_BaiHat);
        }
    }
}