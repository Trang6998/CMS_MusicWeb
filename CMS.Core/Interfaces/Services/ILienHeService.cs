using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface ILienHeService
    {
        public IQueryable<LienHe> GetLienHe(string keywords);
        public Task<LienHe> GetLienHeById(int id);
        public Task CreateLienHe(LienHe lienHe);
        public Task UpdateLienHe(LienHe lienHe);
        public Task DeleteLienHe(int id);
    }
}