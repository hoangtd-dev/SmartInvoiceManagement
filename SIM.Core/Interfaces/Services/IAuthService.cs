using SIM.Core.DTOs.Responses;

namespace SIM.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<UserLoginModel> LoginAsync(string email, string password);
        Task RegisterAsync(string firstName, string lastName, string email, string rawPassword);
    }
}