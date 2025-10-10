
using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;
using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories;
using SIM.Core.Interfaces.Services;

namespace SIM.Core.Services
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) { 
            _userRepository = userRepository;
        }
        public async Task<UserModel> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                // Handle Exception
            }

            return new UserModel {
                Id = id,
                Address = user.Address,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone
            };
        }

        public async Task UpdateUser(UpdateUserRequest user)
        {
            var updatedUser = new User {
                Id = user.Id,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                FirstName = user.Firstname,
                LastName = user.Lastname
            };

            await _userRepository.UpdateAsync(updatedUser);
        }
    }
}
