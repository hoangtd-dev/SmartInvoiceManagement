using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;

namespace SIM.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<ICollection<ProductModel>> GetProducts();
        Task<ProductModel> GetProductById(int id);
        Task AddProduct(CreateProductRequest product);
        Task UpdateProduct(UpdateProductRequest product);
        Task DeleteProduct(int id);
    }
}
