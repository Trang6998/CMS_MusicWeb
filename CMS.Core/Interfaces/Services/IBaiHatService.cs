using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface IBaiHatService
    {
        public IQueryable<BaiHat> GetBaiHat(string keywords);
        public Task<BaiHat> GetBaiHatById(int id);
        public Task CreateBaiHat(BaiHat baiHat);
        public Task UpdateBaiHat(BaiHat baiHat);
        public Task DeleteBaiHat(int id);
    }
}