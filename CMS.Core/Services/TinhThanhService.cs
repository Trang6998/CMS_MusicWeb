using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class TinhThanhService: ITinhThanhService
    {
        private readonly IRepository<TinhThanh> _tinhThanhRepository;
        public TinhThanhService(IRepository<TinhThanh> tinhThanhRepository)
        {
            _tinhThanhRepository = tinhThanhRepository;
        }
        public IQueryable<TinhThanh> GetTinhThanh(string keywords)
        {
            var query = _tinhThanhRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
                query = query
                    .Where(tinhThanh =>
                        tinhThanh.TenTinhThanh.Contains(keywords)
                    );
            }
            return query;
        }

        public async Task<TinhThanh> GetTinhThanhById(int id)
        {
            return await _tinhThanhRepository.GetByIdAsync(id);
        }
        public async Task CreateTinhThanh(TinhThanh tinhThanh)
        {
            await _tinhThanhRepository.AddAsync(tinhThanh);
        }
        public async Task UpdateTinhThanh(TinhThanh tinhThanh)
        {
            await _tinhThanhRepository.UpdateAsync(tinhThanh);
        }
        public async Task DeleteTinhThanh(int id)
        {
            var tinhThanh = await _tinhThanhRepository.GetByIdAsync(id);
            await _tinhThanhRepository.DeleteAsync(tinhThanh);
        }
    }
}