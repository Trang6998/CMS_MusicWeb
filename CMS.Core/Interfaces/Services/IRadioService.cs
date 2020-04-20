using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface IRadioService
    {
        public IQueryable<Radio> GetRadio(string keywords);
        public Task<Radio> GetRadioById(int id);
        public Task CreateRadio(Radio radio);
        public Task UpdateRadio(Radio radio);
        public Task DeleteRadio(int id);
    }
}