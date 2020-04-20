using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class FileUploadService: IFileUploadService
    {
        private readonly IRepository<FileUpload> _fileUploadRepository;
        public FileUploadService(IRepository<FileUpload> fileUploadRepository)
        {
            _fileUploadRepository = fileUploadRepository;
        }
        public IQueryable<FileUpload> GetFileUpload(string keywords)
        {
            var query = _fileUploadRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
                
            }
            return query;
        }

        public async Task<FileUpload> GetFileUploadById(int id)
        {
            return await _fileUploadRepository.GetByIdAsync(id);
        }
        public async Task CreateFileUpload(FileUpload fileUpload)
        {
            await _fileUploadRepository.AddAsync(fileUpload);
        }
        public async Task UpdateFileUpload(FileUpload fileUpload)
        {
            await _fileUploadRepository.UpdateAsync(fileUpload);
        }
        public async Task DeleteFileUpload(int id)
        {
            var fileUpload = await _fileUploadRepository.GetByIdAsync(id);
            await _fileUploadRepository.DeleteAsync(fileUpload);
        }
    }
}