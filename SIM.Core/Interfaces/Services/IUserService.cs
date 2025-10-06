
using SIM.Core.DTOs.Responses;

namespace SIM.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserModel> GetUserById(int id);
    }
}
