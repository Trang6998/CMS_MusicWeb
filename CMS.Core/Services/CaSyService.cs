using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class CaSyService: ICaSyService
    {
        private readonly IRepository<CaSy> _caSyRepository;
        public CaSyService(IRepository<CaSy> caSyRepository)
        {
            _caSyRepository = caSyRepository;
        }
        public IQueryable<CaSy> GetCaSy(string keywords)
        {
            var query = _caSyRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
                query = query
                    .Where(caSy =>
                        caSy.HoTen.Contains(keywords)
                    );
            }
            return query;
        }

        public async Task<CaSy> GetCaSyById(int id)
        {
            return await _caSyRepository.GetByIdAsync(id);
        }
        public async Task CreateCaSy(CaSy caSy)
        {
            await _caSyRepository.AddAsync(caSy);
        }
        public async Task UpdateCaSy(CaSy caSy)
        {
            await _caSyRepository.UpdateAsync(caSy);
        }
        public async Task DeleteCaSy(int id)
        {
            var caSy = await _caSyRepository.GetByIdAsync(id);
            await _caSyRepository.DeleteAsync(caSy);
        }
    }
}