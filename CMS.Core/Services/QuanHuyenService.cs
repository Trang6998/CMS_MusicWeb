using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class QuanHuyenService: IQuanHuyenService
    {
        private readonly IRepository<QuanHuyen> _quanHuyenRepository;
        public QuanHuyenService(IRepository<QuanHuyen> quanHuyenRepository)
        {
            _quanHuyenRepository = quanHuyenRepository;
        }
        public IQueryable<QuanHuyen> GetQuanHuyen(string keywords)
        {
            var query = _quanHuyenRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
                query = query
                    .Where(quanHuyen =>
                        quanHuyen.TenQuanHuyen.Contains(keywords)
                    );
            }
            return query;
        }

        public async Task<QuanHuyen> GetQuanHuyenById(int id)
        {
            return await _quanHuyenRepository.GetByIdAsync(id);
        }
        public async Task CreateQuanHuyen(QuanHuyen quanHuyen)
        {
            await _quanHuyenRepository.AddAsync(quanHuyen);
        }
        public async Task UpdateQuanHuyen(QuanHuyen quanHuyen)
        {
            await _quanHuyenRepository.UpdateAsync(quanHuyen);
        }
        public async Task DeleteQuanHuyen(int id)
        {
            var quanHuyen = await _quanHuyenRepository.GetByIdAsync(id);
            await _quanHuyenRepository.DeleteAsync(quanHuyen);
        }
    }
}