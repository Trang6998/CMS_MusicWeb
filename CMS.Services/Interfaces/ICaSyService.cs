using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface ICaSyService
    {
        IQueryable<CaSy> GetCaSy(string keywords);
        Task<CaSy> GetCaSyById(int id);
        Task CreateCaSy(CaSy caSy);
        Task UpdateCaSy(CaSy caSy);
        Task DeleteCaSy(int id);
    }
}