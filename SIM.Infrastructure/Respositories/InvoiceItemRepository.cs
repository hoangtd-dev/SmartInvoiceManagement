using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories;

namespace SIM.Infrastructure.Respositories
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        public Task<InvoiceItem> AddAsync(InvoiceItem entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<InvoiceItem>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<InvoiceItem?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(InvoiceItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
