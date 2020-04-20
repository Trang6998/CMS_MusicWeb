using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface IEventService
    {
        public IQueryable<Event> GetEvent(string keywords);
        public Task<Event> GetEventById(int id);
        public Task CreateEvent(Event suKien);
        public Task UpdateEvent(Event suKien);
        public Task DeleteEvent(int id);
    }
}