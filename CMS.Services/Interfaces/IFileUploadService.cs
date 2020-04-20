using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface IFileUploadService
    {
        IQueryable<FileUpload> GetFileUpload(string keywords);
        Task<FileUpload> GetFileUploadById(int id);
        Task CreateFileUpload(FileUpload fileUpload);
        Task UpdateFileUpload(FileUpload fileUpload);
        Task DeleteFileUpload(int id);
    }
}