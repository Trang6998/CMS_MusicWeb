using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface ITinhThanhService
    {
        public IQueryable<TinhThanh> GetTinhThanh(string keywords);
        public Task<TinhThanh> GetTinhThanhById(int id);
        public Task CreateTinhThanh(TinhThanh tinhThanh);
        public Task UpdateTinhThanh(TinhThanh tinhThanh);
        public Task DeleteTinhThanh(int id);
    }
}