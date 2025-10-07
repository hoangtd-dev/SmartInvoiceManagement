using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories;

namespace SIM.Infrastructure.Respositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _appDbContext;
        public InvoiceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task<Invoice> AddAsync(Invoice entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Invoice>> GetAllAsync()
        {
            return await _appDbContext.Invoices.ToListAsync();
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
