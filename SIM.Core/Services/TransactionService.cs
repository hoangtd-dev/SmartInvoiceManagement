
using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;
using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories;
using SIM.Core.Interfaces.Services;

namespace SIM.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository) {
            _transactionRepository = transactionRepository;
        }

        public async Task CreateTransaction(CreateTransactionRequest transaction)
        {
            // TODO: Mapping
            await _transactionRepository.AddAsync(new Transaction());
        }

        public async Task DeleteTransaction(int id)
        {
            await _transactionRepository.DeleteAsync(id);
        }

        public async Task<TransactionModel> GetTransactionById(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);

            // TODO: Mapping
            return new TransactionModel();
        }

        public async Task<ICollection<TransactionModel>> GetTransactions()
        {
            var transactions = await _transactionRepository.GetAllAsync();

            return transactions.Select(transaction => new TransactionModel
            {
                Type = transaction.TransactionType,
                TotalAmount = transaction.TotalAmount,
            }).ToList();
        }

        public async Task UpdateTransaction(UpdateTransactionRequest transaction)
        {
            // TODO: Mapping
            await _transactionRepository.UpdateAsync(new Transaction());
        }
    }
}
