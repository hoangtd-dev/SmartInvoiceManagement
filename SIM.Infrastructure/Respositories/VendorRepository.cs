using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories;

namespace SIM.Infrastructure.Respositories
{
    public class VendorRepository : IVendorRespository
    {
        public Task<Vendor> AddAsync(Vendor entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Vendor>> GetAllAsync()
        {
            throw new NotImplementedException();
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
