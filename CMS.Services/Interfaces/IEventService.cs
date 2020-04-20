using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface IEventService
    {
        IQueryable<Event> GetEvent(string keywords);
        Task<Event> GetEventById(int id);
        Task CreateEvent(Event suKien);
        Task UpdateEvent(Event suKien);
        Task DeleteEvent(int id);
    }
}