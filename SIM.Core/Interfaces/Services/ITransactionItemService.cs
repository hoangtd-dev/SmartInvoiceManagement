using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;

namespace SIM.Core.Interfaces.Services
{
    public interface ITransactionItemService
    {
        Task CreateTransactionItem(CreateTransactionItemRequest item);
        Task UpdateTransactionItem(UpdateTransactionItemRequest item);
        Task DeleteTransactionItem(int id);
        Task<TransactionItemModel> GetTransactionItemById(int id);
        Task<ICollection<TransactionItemModel>> GetTransactionItems();
    }
}
