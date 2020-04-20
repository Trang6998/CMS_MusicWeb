using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface IChuyenMuc_BaiVietService
    {
        public IQueryable<ChuyenMuc_BaiViet> GetChuyenMuc_BaiViet(string keywords);
        public Task<ChuyenMuc_BaiViet> GetChuyenMuc_BaiVietById(int id);
        public Task CreateChuyenMuc_BaiViet(ChuyenMuc_BaiViet chuyenMuc_BaiViet);
        public Task UpdateChuyenMuc_BaiViet(ChuyenMuc_BaiViet chuyenMuc_BaiViet);
        public Task DeleteChuyenMuc_BaiViet(int id);
    }
}