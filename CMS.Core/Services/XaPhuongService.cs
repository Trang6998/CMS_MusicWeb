using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class XaPhuongService: IXaPhuongService
    {
        private readonly IRepository<XaPhuong> _xaPhuongRepository;
        public XaPhuongService(IRepository<XaPhuong> xaPhuongRepository)
        {
            _xaPhuongRepository = xaPhuongRepository;
        }
        public IQueryable<XaPhuong> GetXaPhuong(string keywords)
        {
            var query = _xaPhuongRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
                query = query
                    .Where(xaPhuong =>
                        xaPhuong.TenXaPhuong.Contains(keywords)
                    );
            }
            return query;
        }

        public async Task<XaPhuong> GetXaPhuongById(int id)
        {
            return await _xaPhuongRepository.GetByIdAsync(id);
        }
        public async Task CreateXaPhuong(XaPhuong xaPhuong)
        {
            await _xaPhuongRepository.AddAsync(xaPhuong);
        }
        public async Task UpdateXaPhuong(XaPhuong xaPhuong)
        {
            await _xaPhuongRepository.UpdateAsync(xaPhuong);
        }
        public async Task DeleteXaPhuong(int id)
        {
            var xaPhuong = await _xaPhuongRepository.GetByIdAsync(id);
            await _xaPhuongRepository.DeleteAsync(xaPhuong);
        }
    }
}