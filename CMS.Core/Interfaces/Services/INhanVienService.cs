using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface INhanVienService
    {
        public IQueryable<NhanVien> GetNhanVien(string keywords);
        public Task<NhanVien> GetNhanVienById(int id);
        public Task CreateNhanVien(NhanVien nhanVien);
        public Task UpdateNhanVien(NhanVien nhanVien);
        public Task DeleteNhanVien(int id);
    }
}