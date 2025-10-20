using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories;

namespace SIM.Infrastructure.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly AppDbContext _appDbContext;
        public VendorRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Vendor> AddAsync(Vendor entity)
        {
            var vendor = await _appDbContext.Vendors.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return vendor.Entity;
        }

        public async Task DeleteAsync(Vendor entity)
        {
            entity.IsDeleted = true;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Vendor>> GetAllAsync()
        {
            return await _appDbContext.Vendors
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<Vendor?> GetByIdAsync(int id)
        {
            return await _appDbContext.Vendors
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Vendor entity)
        {
            _appDbContext.Vendors.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
