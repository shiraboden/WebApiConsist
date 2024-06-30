using WebApiConsist.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApiConsist.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        public UserRepository(UserDbContext context)
        {
            _context = context;
        }
        public async Task<User?> CreateUserAsync(string name, string password)
        {
            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();
            //return user;
            var user = new User
            {
                UserName = name,
                UserPassword = password
            };
            if (await ValidateUserAsync(name, password))
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
        }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ValidateUserAsync(string userName, string userPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.UserPassword == userPassword);
            if (user == null)
                return true;
            return false;
        }
        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
