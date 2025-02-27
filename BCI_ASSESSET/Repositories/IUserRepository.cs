using BCI_ASSESSET.Models;

namespace BCI_ASSESSET.Repositories
{
    public interface IUserRepository
    {
        Task<User> getUserByUsername(string Username);
    }
}
