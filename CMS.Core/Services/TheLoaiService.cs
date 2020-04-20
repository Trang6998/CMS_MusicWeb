using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class TheLoaiService: ITheLoaiService
    {
        private readonly IRepository<TheLoai> _theLoaiRepository;
        public TheLoaiService(IRepository<TheLoai> theLoaiRepository)
        {
            _theLoaiRepository = theLoaiRepository;
        }
        public IQueryable<TheLoai> GetTheLoai(string keywords)
        {
            var query = _theLoaiRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
                query = query
                    .Where(theLoai =>
                        theLoai.TenTheLoai.Contains(keywords)
                    );
            }
            return query;
        }

        public async Task<TheLoai> GetTheLoaiById(int id)
        {
            return await _theLoaiRepository.GetByIdAsync(id);
        }
        public async Task CreateTheLoai(TheLoai theLoai)
        {
            await _theLoaiRepository.AddAsync(theLoai);
        }
        public async Task UpdateTheLoai(TheLoai theLoai)
        {
            await _theLoaiRepository.UpdateAsync(theLoai);
        }
        public async Task DeleteTheLoai(int id)
        {
            var theLoai = await _theLoaiRepository.GetByIdAsync(id);
            await _theLoaiRepository.DeleteAsync(theLoai);
        }
    }
}