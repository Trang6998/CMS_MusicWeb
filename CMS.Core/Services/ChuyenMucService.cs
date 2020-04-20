using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class ChuyenMucService: IChuyenMucService
    {
        private readonly IRepository<ChuyenMuc> _chuyenMucRepository;
        public ChuyenMucService(IRepository<ChuyenMuc> chuyenMucRepository)
        {
            _chuyenMucRepository = chuyenMucRepository;
        }
        public IQueryable<ChuyenMuc> GetChuyenMuc(string keywords)
        {
            var query = _chuyenMucRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
                query = query
                    .Where(chuyenMuc =>
                        chuyenMuc.TenChuyenMuc.Contains(keywords)
                    );
            }
            return query;
        }

        public async Task<ChuyenMuc> GetChuyenMucById(int id)
        {
            return await _chuyenMucRepository.GetByIdAsync(id);
        }
        public async Task CreateChuyenMuc(ChuyenMuc chuyenMuc)
        {
            await _chuyenMucRepository.AddAsync(chuyenMuc);
        }
        public async Task UpdateChuyenMuc(ChuyenMuc chuyenMuc)
        {
            await _chuyenMucRepository.UpdateAsync(chuyenMuc);
        }
        public async Task DeleteChuyenMuc(int id)
        {
            var chuyenMuc = await _chuyenMucRepository.GetByIdAsync(id);
            await _chuyenMucRepository.DeleteAsync(chuyenMuc);
        }
    }
}