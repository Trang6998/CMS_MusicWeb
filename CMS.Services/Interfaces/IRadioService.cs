using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface IRadioService
    {
        IQueryable<Radio> GetRadio(string keywords);
        Task<Radio> GetRadioById(int id);
        Task CreateRadio(Radio radio);
        Task UpdateRadio(Radio radio);
        Task DeleteRadio(int id);
    }
}