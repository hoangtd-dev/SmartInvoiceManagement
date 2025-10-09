using SIM.Core.Entities;

namespace SIM.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<User> LoginAsync(string email, string password);
        Task<User> RegisterAsync(string firstName, string lastName, string email, string rawPassword);
    }
}