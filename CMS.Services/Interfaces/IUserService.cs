using CMS.Models;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Services.Interfaces
{
    public interface IUserService
    {
        IQueryable<User> GetUser(string keywords);
        Task<User> GetUserById(int id);
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
    }
}