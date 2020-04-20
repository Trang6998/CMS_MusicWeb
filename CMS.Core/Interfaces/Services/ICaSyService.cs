using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface ICaSyService
    {
        public IQueryable<CaSy> GetCaSy(string keywords);
        public Task<CaSy> GetCaSyById(int id);
        public Task CreateCaSy(CaSy caSy);
        public Task UpdateCaSy(CaSy caSy);
        public Task DeleteCaSy(int id);
    }
}