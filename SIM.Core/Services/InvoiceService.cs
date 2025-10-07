
using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;
using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories;
using SIM.Core.Interfaces.Services;

namespace SIM.Core.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceService(IInvoiceRepository invoiceRepository) {
            _invoiceRepository = invoiceRepository;
        }

        public async Task CreateInvoice(CreateInvoiceRequest invoice)
        {
            // TODO: Mapping
            await _invoiceRepository.AddAsync(new Invoice());
        }

        public async Task DeleteInvoice(int id)
        {
            await _invoiceRepository.DeleteAsync(id);
        }

        public async Task<InvoiceModel> GetInvoiceById(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);

            // TODO: Mapping
            return new InvoiceModel();
        }

        public async Task<ICollection<InvoiceModel>> GetInvoices()
        {
            var invoices = await _invoiceRepository.GetAllAsync();

            return invoices.Select(invoice => new InvoiceModel
            {
                Category = invoice.Category,
                Status = invoice.Status,
                TotalAmount = invoice.TotalAmount,
            }).ToList();
        }

        public async Task UpdateInvoice(UpdateInvoiceRequest invoice)
        {
            // TODO: Mapping
            await _invoiceRepository.UpdateAsync(new Invoice());
        }
    }
}
