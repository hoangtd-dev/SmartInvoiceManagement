using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;

namespace SIM.Core.Interfaces.Services
{
    public interface ITransactionCategoryService
    {
        Task<ICollection<TransactionCategoryModel>> GetTransactionCategories();
        Task<TransactionCategoryModel> GetCategoryById(int id);
        Task AddCategory(CreateCategoryRequest vendor);
        Task UpdateCategory(UpdateCategoryRequest vendor);
        Task DeleteCategory(int id);
    }
}
