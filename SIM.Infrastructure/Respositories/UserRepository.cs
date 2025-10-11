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
        public async Task<User> AddAsync(User entity)
        {
            var entry = await _appDbContext.Users.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entry.Entity;        }

        public async Task DeleteAsync(User entity)
        {
            entity.IsDeleted = true;
            await _appDbContext.SaveChangesAsync();
        }

        public Task<ICollection<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task UpdateAsync(User user)
        {
            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
