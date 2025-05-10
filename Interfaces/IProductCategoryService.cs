using EasyCommerce.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyCommerce.Interfaces
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategory>> GetAllAsync();
        Task AddAsync(ProductCategory pc);
        Task DeleteAsync(int productId, int categoryId);
    }
}
