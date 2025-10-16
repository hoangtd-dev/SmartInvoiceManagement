using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;
using SIM.Core.Entities;
using SIM.Core.Exceptions;
using SIM.Core.Interfaces.Repositories;
using SIM.Core.Interfaces.Services;

namespace SIM.Core.Services
{
    public class TransactionItemService : ITransactionItemService
    {
        private readonly ITransactionItemRepository _repo;
        public TransactionItemService(ITransactionItemRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateTransactionItem(CreateTransactionItemRequest item)
        {
            var entity = new TransactionItem
            {
                TransactionId = item.TransactionId,
                Quantity = item.Quantity,
                Price = item.Price,
                Total = item.Total,
                CreatedDate = DateTime.UtcNow
            };

            await _repo.AddAsync(entity);
        }

        public async Task DeleteTransactionItem(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) throw new NotFoundException($"TransactionItem with id:{id} is not found !!!");

            await _repo.DeleteAsync(entity);
        }

        public async Task<TransactionItemModel> GetTransactionItemById(int id)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e is null) throw new NotFoundException($"TransactionItem with id:{id} is not found !!!");

            return new TransactionItemModel
            {
                Id = e.Id,
                TransactionId = e.TransactionId,
                Quantity = e.Quantity,
                Price = e.Price,
                Total = e.Total
            };
        }

        public async Task<ICollection<TransactionItemModel>> GetTransactionItems()
        {
            var items = await _repo.GetAllAsync();
            return items.Select(e => new TransactionItemModel
            {
                Id = e.Id,
                TransactionId = e.TransactionId,
                Quantity = e.Quantity,
                Price = e.Price,
                Total = e.Total
            }).ToList();
        }

        public async Task UpdateTransactionItem(UpdateTransactionItemRequest item)
        {
            var existing = await _repo.GetByIdAsync(item.Id);
            if (existing is null) throw new NotFoundException($"TransactionItem with id:{item.Id} is not found !!!");

            existing.Quantity = item.Quantity;
            existing.Price = item.Price;
            existing.Total = item.Total;

            await _repo.UpdateAsync(existing);
        }
    }
}
