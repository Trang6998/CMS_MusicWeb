using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface ILoaiNhanVienService
    {
        public IQueryable<LoaiNhanVien> GetLoaiNhanVien(string keywords);
        public Task<LoaiNhanVien> GetLoaiNhanVienById(int id);
        public Task CreateLoaiNhanVien(LoaiNhanVien loaiNhanVien);
        public Task UpdateLoaiNhanVien(LoaiNhanVien loaiNhanVien);
        public Task DeleteLoaiNhanVien(int id);
    }
}