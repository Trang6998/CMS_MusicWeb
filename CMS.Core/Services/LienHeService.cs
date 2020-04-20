using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class LienHeService: ILienHeService
    {
        private readonly IRepository<LienHe> _lienHeRepository;
        public LienHeService(IRepository<LienHe> lienHeRepository)
        {
            _lienHeRepository = lienHeRepository;
        }
        public IQueryable<LienHe> GetLienHe(string keywords)
        {
            var query = _lienHeRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
                query = query
                    .Where(lienHe =>
                        lienHe.HoTen.Contains(keywords)
                    );
            }
            return query;
        }

        public async Task<LienHe> GetLienHeById(int id)
        {
            return await _lienHeRepository.GetByIdAsync(id);
        }
        public async Task CreateLienHe(LienHe lienHe)
        {
            await _lienHeRepository.AddAsync(lienHe);
        }
        public async Task UpdateLienHe(LienHe lienHe)
        {
            await _lienHeRepository.UpdateAsync(lienHe);
        }
        public async Task DeleteLienHe(int id)
        {
            var lienHe = await _lienHeRepository.GetByIdAsync(id);
            await _lienHeRepository.DeleteAsync(lienHe);
        }
    }
}