using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface IFileUploadService
    {
        public IQueryable<FileUpload> GetFileUpload(string keywords);
        public Task<FileUpload> GetFileUploadById(int id);
        public Task CreateFileUpload(FileUpload fileUpload);
        public Task UpdateFileUpload(FileUpload fileUpload);
        public Task DeleteFileUpload(int id);
    }
}