using SIM.Core.DTOs.Responses;
using SIM.Core.Interfaces.Repositories;
using SIM.Core.Interfaces.Services;

namespace SIM.Core.Services
{
    public class TransactionCategoryService : ITransactionCategoryService
    {
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        public TransactionCategoryService(ITransactionCategoryRepository transactionCategoryRepository)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
        }
        public async Task<ICollection<TransactionCategoryModel>> GetTransactionCategories()
        {
            var transactionItems = await _transactionCategoryRepository.GetAllAsync();

            return transactionItems.Select(i => new TransactionCategoryModel { 
                Id = i.Id,
                Name = i.Name,
                Description = i.Description
            }).ToList();
        }
    }
}
