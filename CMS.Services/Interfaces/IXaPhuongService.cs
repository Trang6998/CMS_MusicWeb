using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface IXaPhuongService
    {
        IQueryable<XaPhuong> GetXaPhuong(string keywords);
        Task<XaPhuong> GetXaPhuongById(int id);
        Task CreateXaPhuong(XaPhuong xaPhuong);
        Task UpdateXaPhuong(XaPhuong xaPhuong);
        Task DeleteXaPhuong(int id);
    }
}