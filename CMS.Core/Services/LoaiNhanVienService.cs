using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class LoaiNhanVienService: ILoaiNhanVienService
    {
        private readonly IRepository<LoaiNhanVien> _loaiNhanVienRepository;
        public LoaiNhanVienService(IRepository<LoaiNhanVien> loaiNhanVienRepository)
        {
            _loaiNhanVienRepository = loaiNhanVienRepository;
        }
        public IQueryable<LoaiNhanVien> GetLoaiNhanVien(string keywords)
        {
            var query = _loaiNhanVienRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
                query = query
                    .Where(loaiNhanVien =>
                        loaiNhanVien.TenLoai.Contains(keywords)
                    );
            }
            return query;
        }

        public async Task<LoaiNhanVien> GetLoaiNhanVienById(int id)
        {
            return await _loaiNhanVienRepository.GetByIdAsync(id);
        }
        public async Task CreateLoaiNhanVien(LoaiNhanVien loaiNhanVien)
        {
            await _loaiNhanVienRepository.AddAsync(loaiNhanVien);
        }
        public async Task UpdateLoaiNhanVien(LoaiNhanVien loaiNhanVien)
        {
            await _loaiNhanVienRepository.UpdateAsync(loaiNhanVien);
        }
        public async Task DeleteLoaiNhanVien(int id)
        {
            var loaiNhanVien = await _loaiNhanVienRepository.GetByIdAsync(id);
            await _loaiNhanVienRepository.DeleteAsync(loaiNhanVien);
        }
    }
}