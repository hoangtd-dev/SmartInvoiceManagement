using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;
using SIM.Core.Entities;
using SIM.Core.Exceptions;
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

        public async Task AddProduct(CreateProductRequest product)
        {
            var newProduct = new Product
            {
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                VendorId = product.VendorId,
            };
            await _productRepository.AddAsync(newProduct);
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null) throw new NotFoundException($"Product with id:{id} is not found !!!");

            await _productRepository.DeleteAsync(product);
        }

        public async Task<ProductModel> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null) throw new NotFoundException($"Product with id:{id} is not found !!!");

            return new ProductModel 
            { 
                Id = product.Id,
                Description = product.Description,
                Name = product.ProductName,
                Price = product.Price,
                StockQuantity= product.StockQuantity,
                VendorId = product.VendorId,
                Vendor = new VendorModel { 
                    Id = product.Vendor.Id,
                    Name = product.Vendor.VendorName
                }
            };
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
                Vendor = new VendorModel
                {
                    Id = x.Vendor.Id,
                    Name = x.Vendor.VendorName
                }
            }).ToList();
        }

        public async Task UpdateProduct(UpdateProductRequest updatedProduct)
        {
            var product = await _productRepository.GetByIdAsync(updatedProduct.Id);

            if (product is null) throw new NotFoundException($"Product with id:{updatedProduct!.Id} is not found !!!");

            product.ProductName = updatedProduct.ProductName;
            product.Price = updatedProduct.Price;
            product.Description = updatedProduct.Description;
            product.StockQuantity = updatedProduct.StockQuantity;
            product.VendorId = updatedProduct.VendorId;

            await _productRepository.UpdateAsync(product);
        }
    }
}
