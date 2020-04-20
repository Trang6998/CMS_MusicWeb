using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class EventService: IEventService
    {
        private readonly IRepository<Event> _eventRepository;
        public EventService(IRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public IQueryable<Event> GetEvent(string keywords)
        {
            var query = _eventRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
              
            }
            return query;
        }

        public async Task<Event> GetEventById(int id)
        {
            return await _eventRepository.GetByIdAsync(id);
        }
        public async Task CreateEvent(Event suKien)
        {
            await _eventRepository.AddAsync(suKien);
        }
        public async Task UpdateEvent(Event suKien)
        {
            await _eventRepository.UpdateAsync(suKien);
        }
        public async Task DeleteEvent(int id)
        {
            var suKien = await _eventRepository.GetByIdAsync(id);
            await _eventRepository.DeleteAsync(suKien);
        }
    }
}