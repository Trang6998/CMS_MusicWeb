using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class ChuyenMuc_BaiVietService: IChuyenMuc_BaiVietService
    {
        private readonly IRepository<ChuyenMuc_BaiViet> _chuyenMuc_BaiVietRepository;
        public ChuyenMuc_BaiVietService(IRepository<ChuyenMuc_BaiViet> chuyenMuc_BaiVietRepository)
        {
            _chuyenMuc_BaiVietRepository = chuyenMuc_BaiVietRepository;
        }
        public IQueryable<ChuyenMuc_BaiViet> GetChuyenMuc_BaiViet(string keywords)
        {
            var query = _chuyenMuc_BaiVietRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
                
            }
            return query;
        }

        public async Task<ChuyenMuc_BaiViet> GetChuyenMuc_BaiVietById(int id)
        {
            return await _chuyenMuc_BaiVietRepository.GetByIdAsync(id);
        }
        public async Task CreateChuyenMuc_BaiViet(ChuyenMuc_BaiViet chuyenMuc_BaiViet)
        {
            await _chuyenMuc_BaiVietRepository.AddAsync(chuyenMuc_BaiViet);
        }
        public async Task UpdateChuyenMuc_BaiViet(ChuyenMuc_BaiViet chuyenMuc_BaiViet)
        {
            await _chuyenMuc_BaiVietRepository.UpdateAsync(chuyenMuc_BaiViet);
        }
        public async Task DeleteChuyenMuc_BaiViet(int id)
        {
            var chuyenMuc_BaiViet = await _chuyenMuc_BaiVietRepository.GetByIdAsync(id);
            await _chuyenMuc_BaiVietRepository.DeleteAsync(chuyenMuc_BaiViet);
        }
    }
}