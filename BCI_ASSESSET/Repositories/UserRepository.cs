using BCI_ASSESSET.DB;
using BCI_ASSESSET.Models;
using Microsoft.EntityFrameworkCore;

namespace BCI_ASSESSET.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> getUserByUsername(string Username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == Username);
        }
    }
}
