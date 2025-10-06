using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories;

namespace SIM.Infrastructure.Respositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public Task<Invoice> AddAsync(Invoice entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Invoice>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Invoice?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Invoice entity)
        {
            throw new NotImplementedException();
        }
    }
}
