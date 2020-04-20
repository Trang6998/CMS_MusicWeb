using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface IQuanHuyenService
    {
        IQueryable<QuanHuyen> GetQuanHuyen(string keywords);
        Task<QuanHuyen> GetQuanHuyenById(int id);
        Task CreateQuanHuyen(QuanHuyen quanHuyen);
        Task UpdateQuanHuyen(QuanHuyen quanHuyen);
        Task DeleteQuanHuyen(int id);
    }
}