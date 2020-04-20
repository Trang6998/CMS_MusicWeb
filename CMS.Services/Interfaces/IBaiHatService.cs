using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface IBaiHatService
    {
        IQueryable<BaiHat> GetBaiHat(string keywords);
        BaiHat GetBaiHatById(int id);
    }
}