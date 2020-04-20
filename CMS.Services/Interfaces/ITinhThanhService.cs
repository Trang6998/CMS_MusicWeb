using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface ITinhThanhService
    {
        IQueryable<TinhThanh> GetTinhThanh(string keywords);
        Task<TinhThanh> GetTinhThanhById(int id);
        Task CreateTinhThanh(TinhThanh tinhThanh);
        Task UpdateTinhThanh(TinhThanh tinhThanh);
        Task DeleteTinhThanh(int id);
    }
}