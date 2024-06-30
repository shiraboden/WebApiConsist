
using WebApiConsist.Models;

namespace WebApiConsist.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(string userName, string userPassword);
        Task<bool> DeleteUserAsync(int userId);
        Task<bool> ValidateUserAsync(string userName, string userPassword);
        Task<List<User>> GetUsers();

    }
}
