using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Core.Interfaces.Services;
namespace CMS.Core.Services
{
    public class UserService: IUserService
    {
        private readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public IQueryable<User> GetUser(string keywords)
        {
            var query = _userRepository.TableUntracked;

            if (!string.IsNullOrEmpty(keywords))
            {
                query = query
                    .Where(user =>
                        user.UserName.Contains(keywords)
                    );
            }
            return query;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
        public async Task CreateUser(User user)
        {
            await _userRepository.AddAsync(user);
        }
        public async Task UpdateUser(User user)
        {
            await _userRepository.UpdateAsync(user);
        }
        public async Task DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            await _userRepository.DeleteAsync(user);
        }
    }
}