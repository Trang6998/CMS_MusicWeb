using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface ILienHeService
    {
        IQueryable<LienHe> GetLienHe(string keywords);
        Task<LienHe> GetLienHeById(int id);
        Task CreateLienHe(LienHe lienHe);
        Task UpdateLienHe(LienHe lienHe);
        Task DeleteLienHe(int id);
    }
}