using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class NhanVienService: INhanVienService
    {
        private readonly IRepository<NhanVien> _nhanVienRepository;
        public NhanVienService(IRepository<NhanVien> nhanVienRepository)
        {
            _nhanVienRepository = nhanVienRepository;
        }
        public IQueryable<NhanVien> GetNhanVien(string keywords)
        {
            var query = _nhanVienRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
                query = query
                    .Where(nhanVien =>
                        nhanVien.HoTen.Contains(keywords)
                    );
            }
            return query;
        }

        public async Task<NhanVien> GetNhanVienById(int id)
        {
            return await _nhanVienRepository.GetByIdAsync(id);
        }
        public async Task CreateNhanVien(NhanVien nhanVien)
        {
            await _nhanVienRepository.AddAsync(nhanVien);
        }
        public async Task UpdateNhanVien(NhanVien nhanVien)
        {
            await _nhanVienRepository.UpdateAsync(nhanVien);
        }
        public async Task DeleteNhanVien(int id)
        {
            var nhanVien = await _nhanVienRepository.GetByIdAsync(id);
            await _nhanVienRepository.DeleteAsync(nhanVien);
        }
    }
}