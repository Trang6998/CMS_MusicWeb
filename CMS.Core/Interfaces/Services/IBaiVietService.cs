using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface IBaiVietService
    {
        public IQueryable<BaiViet> GetBaiViet(string keywords);
        public Task<BaiViet> GetBaiVietById(int id);
        public Task CreateBaiViet(BaiViet baiViet);
        public Task UpdateBaiViet(BaiViet baiViet);
        public Task DeleteBaiViet(int id);
    }
}