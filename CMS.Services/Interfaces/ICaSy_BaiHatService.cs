using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface ICaSy_BaiHatService
    {
        IQueryable<CaSy_BaiHat> GetCaSy_BaiHat(string keywords);
        Task<CaSy_BaiHat> GetCaSy_BaiHatById(int id);
        Task CreateCaSy_BaiHat(CaSy_BaiHat caSy_BaiHat);
        Task UpdateCaSy_BaiHat(CaSy_BaiHat caSy_BaiHat);
        Task DeleteCaSy_BaiHat(int id);
    }
}