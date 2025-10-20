
using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;
using SIM.Core.Exceptions;
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

            if (user is null) throw new NotFoundException($"User with id:{id} is not found !!!");

            return new UserModel {
                Id = id,
                Address = user.Address,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone
            };
        }
        
        public async Task<UserModel> GetUserByEmail(string email)
        {
             var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
            {
                // Handle Exception
            }

            return new UserModel { 
                Address = user.Address,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone
            };
        }

        public async Task UpdateUser(UpdateUserRequest updatedUser)
        {
            var user = await _userRepository.GetByIdAsync(updatedUser.Id);

            if (user is null) throw new NotFoundException($"User with id:{updatedUser.Id} is not found !!!");

            user.FirstName = updatedUser.Firstname;
            user.LastName = updatedUser.Lastname;
            user.Phone = updatedUser.Phone;
            user.Email = updatedUser.Email;
            user.Address = updatedUser.Address;

            await _userRepository.UpdateAsync(user);
        }
    }
}
