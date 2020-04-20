using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface IBaiVietService
    {
        IQueryable<BaiViet> GetBaiViet(string keywords);
        Task<BaiViet> GetBaiVietById(int id);
        Task CreateBaiViet(BaiViet baiViet);
        Task UpdateBaiViet(BaiViet baiViet);
        Task DeleteBaiViet(int id);
    }
}