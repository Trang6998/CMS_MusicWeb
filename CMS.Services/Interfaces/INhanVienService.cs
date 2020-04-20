using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface INhanVienService
    {
        IQueryable<NhanVien> GetNhanVien(string keywords);
        Task<NhanVien> GetNhanVienById(int id);
        Task CreateNhanVien(NhanVien nhanVien);
        Task UpdateNhanVien(NhanVien nhanVien);
        Task DeleteNhanVien(int id);
    }
}