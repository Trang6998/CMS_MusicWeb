using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface ICaSy_BaiHatService
    {
        public IQueryable<CaSy_BaiHat> GetCaSy_BaiHat(string keywords);
        public Task<CaSy_BaiHat> GetCaSy_BaiHatById(int id);
        public Task CreateCaSy_BaiHat(CaSy_BaiHat caSy_BaiHat);
        public Task UpdateCaSy_BaiHat(CaSy_BaiHat caSy_BaiHat);
        public Task DeleteCaSy_BaiHat(int id);
    }
}