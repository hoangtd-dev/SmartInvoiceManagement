using SIM.Core.DTOs.Responses;
using SIM.Core.Interfaces.Repositories;
using SIM.Core.Interfaces.Services;

namespace SIM.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ICollection<ProductModel>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();

            return products.Select(x => new ProductModel
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.ProductName,
                Price = x.Price,
                StockQuantity = x.StockQuantity,
                VendorName = x.Vendor.VendorName
            }).ToList();
        }
    }
}
