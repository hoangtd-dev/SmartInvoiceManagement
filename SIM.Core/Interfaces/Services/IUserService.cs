
using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;

namespace SIM.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserModel> GetUserById(int id);
        Task UpdateUser(UpdateUserRequest user);
    }
}
