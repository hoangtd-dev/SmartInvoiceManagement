using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories;

namespace SIM.Infrastructure.Respositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task<User> AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(User user)
        {
            var updatedUser = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (updatedUser != null) {
                // Handle exception
            }

            updatedUser.Email = user.Email;
            updatedUser.Address = user.Address;
            updatedUser.Phone = user.Phone;
            updatedUser.FirstName = user.FirstName;
            updatedUser.LastName = user.LastName;

            await _appDbContext.SaveChangesAsync();
        }
    }
}
