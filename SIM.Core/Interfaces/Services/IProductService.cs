using SIM.Core.DTOs.Responses;

namespace SIM.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<ICollection<ProductModel>> GetProducts();
    }
}
