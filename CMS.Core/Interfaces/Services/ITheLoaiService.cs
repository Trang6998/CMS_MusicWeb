using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface ITheLoaiService
    {
        public IQueryable<TheLoai> GetTheLoai(string keywords);
        public Task<TheLoai> GetTheLoaiById(int id);
        public Task CreateTheLoai(TheLoai theLoai);
        public Task UpdateTheLoai(TheLoai theLoai);
        public Task DeleteTheLoai(int id);
    }
}