using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface IXaPhuongService
    {
        public IQueryable<XaPhuong> GetXaPhuong(string keywords);
        public Task<XaPhuong> GetXaPhuongById(int id);
        public Task CreateXaPhuong(XaPhuong xaPhuong);
        public Task UpdateXaPhuong(XaPhuong xaPhuong);
        public Task DeleteXaPhuong(int id);
    }
}