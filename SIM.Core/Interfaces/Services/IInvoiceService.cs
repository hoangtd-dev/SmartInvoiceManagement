
using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;

namespace SIM.Core.Interfaces.Services
{
    public interface IInvoiceService
    {
        Task CreateInvoice(CreateInvoiceRequest invoice);
        Task UpdateInvoice(UpdateInvoiceRequest invoice);
        Task DeleteInvoice(int id);
        Task<InvoiceModel> GetInvoiceById(int id);
        Task<ICollection<InvoiceModel>> GetInvoices(); // TODO: Add filter
    }
}
