using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class RadioService: IRadioService
    {
        private readonly IRepository<Radio> _radioRepository;
        public RadioService(IRepository<Radio> radioRepository)
        {
            _radioRepository = radioRepository;
        }
        public IQueryable<Radio> GetRadio(string keywords)
        {
            var query = _radioRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
                query = query
                    .Where(radio =>
                        radio.TieuDe.Contains(keywords)
                    );
            }
            return query;
        }

        public async Task<Radio> GetRadioById(int id)
        {
            return await _radioRepository.GetByIdAsync(id);
        }
        public async Task CreateRadio(Radio radio)
        {
            await _radioRepository.AddAsync(radio);
        }
        public async Task UpdateRadio(Radio radio)
        {
            await _radioRepository.UpdateAsync(radio);
        }
        public async Task DeleteRadio(int id)
        {
            var radio = await _radioRepository.GetByIdAsync(id);
            await _radioRepository.DeleteAsync(radio);
        }
    }
}