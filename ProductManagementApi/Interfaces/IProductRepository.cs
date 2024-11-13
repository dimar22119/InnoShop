using ProductManagementApi.Dtos;
using ProductManagementApi.Helpers;
using ProductManagementApi.Models;

namespace ProductManagementApi.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(QueryObject query);
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product productModel);
        Task<Product?> UpdateAsync(int id, UpdateProductDto productDto);
        Task<Product?> DeleteAsync(int id);
    }
}
