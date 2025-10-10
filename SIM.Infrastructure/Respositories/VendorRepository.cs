using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories;

namespace SIM.Infrastructure.Respositories
{
    public class VendorRepository : IVendorRespository
    {
        private readonly AppDbContext _appDbContext;
        public VendorRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task<Vendor> AddAsync(Vendor entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Vendor>> GetAllAsync()
        {
            return await _appDbContext.Vendors.ToListAsync();
        }

        public Task<Vendor?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Vendor entity)
        {
            throw new NotImplementedException();
        }
    }
}
