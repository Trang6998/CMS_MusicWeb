using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface IChuyenMuc_BaiVietService
    {
        IQueryable<ChuyenMuc_BaiViet> GetChuyenMuc_BaiViet(string keywords);
        Task<ChuyenMuc_BaiViet> GetChuyenMuc_BaiVietById(int id);
        Task CreateChuyenMuc_BaiViet(ChuyenMuc_BaiViet chuyenMuc_BaiViet);
        Task UpdateChuyenMuc_BaiViet(ChuyenMuc_BaiViet chuyenMuc_BaiViet);
        Task DeleteChuyenMuc_BaiViet(int id);
    }
}