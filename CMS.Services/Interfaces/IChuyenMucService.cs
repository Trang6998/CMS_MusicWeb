using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface IChuyenMucService
    {
        IQueryable<ChuyenMuc> GetChuyenMuc(string keywords);
        IQueryable<ChuyenMuc> GetChuyenMucNoiBat();
        ChuyenMuc GetChuyenMucById(int id);
    }
}