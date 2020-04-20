using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface IQuanHuyenService
    {
        public IQueryable<QuanHuyen> GetQuanHuyen(string keywords);
        public Task<QuanHuyen> GetQuanHuyenById(int id);
        public Task CreateQuanHuyen(QuanHuyen quanHuyen);
        public Task UpdateQuanHuyen(QuanHuyen quanHuyen);
        public Task DeleteQuanHuyen(int id);
    }
}