using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface IChuyenMucService
    {
        public IQueryable<ChuyenMuc> GetChuyenMuc(string keywords);
        public Task<ChuyenMuc> GetChuyenMucById(int id);
        public Task CreateChuyenMuc(ChuyenMuc chuyenMuc);
        public Task UpdateChuyenMuc(ChuyenMuc chuyenMuc);
        public Task DeleteChuyenMuc(int id);
    }
}