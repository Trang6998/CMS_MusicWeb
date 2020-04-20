using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface ILoaiNhanVienService
    {
        IQueryable<LoaiNhanVien> GetLoaiNhanVien(string keywords);
        Task<LoaiNhanVien> GetLoaiNhanVienById(int id);
        Task CreateLoaiNhanVien(LoaiNhanVien loaiNhanVien);
        Task UpdateLoaiNhanVien(LoaiNhanVien loaiNhanVien);
        Task DeleteLoaiNhanVien(int id);
    }
}