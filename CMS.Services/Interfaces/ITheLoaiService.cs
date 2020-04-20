using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface ITheLoaiService
    {
        IQueryable<TheLoai> GetTheLoai(string keywords);
        Task<TheLoai> GetTheLoaiById(int id);
        Task CreateTheLoai(TheLoai theLoai);
        Task UpdateTheLoai(TheLoai theLoai);
        Task DeleteTheLoai(int id);
    }
}