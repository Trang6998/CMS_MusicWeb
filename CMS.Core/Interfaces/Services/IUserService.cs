using CMS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.Core.Interfaces.Services
{
    public interface IUserService
    {
        public IQueryable<User> GetUser(string keywords);
        public Task<User> GetUserById(int id);
        public Task CreateUser(User user);
        public Task UpdateUser(User user);
        public Task DeleteUser(int id);
    }
}