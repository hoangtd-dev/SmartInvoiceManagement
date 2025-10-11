using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;
using SIM.Core.Entities;
using SIM.Core.Exceptions;
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

        public async Task AddCategory(CreateCategoryRequest category)
        {
            var newCategory = new TransactionCategory
            { 
                Name = category.Name,
                Description = category.Description,
            };
            await _transactionCategoryRepository.AddAsync(newCategory);
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _transactionCategoryRepository.GetByIdAsync(id);

            if (category is null) throw new NotFoundException($"Category with id:{id} is not found !!!");

            await _transactionCategoryRepository.DeleteAsync(category);
        }

        public async Task<TransactionCategoryModel> GetCategoryById(int id)
        {
            var category = await _transactionCategoryRepository.GetByIdAsync(id);

            if (category is null) throw new NotFoundException($"Category with id:{id} is not found !!!");

            return new TransactionCategoryModel
            { 
                Id = id,
                Name = category.Name,
                Description = category.Description,
            };
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

        public async Task UpdateCategory(UpdateCategoryRequest updatedCategory)
        {
            var category = await _transactionCategoryRepository.GetByIdAsync(updatedCategory.Id);

            if (category is null) throw new NotFoundException($"Category with id:{category.Id} is not found !!!");

            category.Name = updatedCategory.Name;
            category.Description = updatedCategory.Description;

            await _transactionCategoryRepository.UpdateAsync(category);
        }
    }
}
