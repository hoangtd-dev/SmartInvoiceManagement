using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories;

namespace SIM.Infrastructure.Respositories
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        private readonly AppDbContext _appDbContext;
        public InvoiceItemRepository(AppDbContext appDbContext) 
        { 
            _appDbContext = appDbContext;
        }
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
