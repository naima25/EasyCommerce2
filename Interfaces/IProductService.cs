using EasyCommerce.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyCommerce.DTO;

namespace EasyCommerce.Interfaces
{
    public interface IProductService
    {
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(int id, Product product);
    Task DeleteProductAsync(int id);
    
 }
}

