using EasyCommerce.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyCommerce.DTO;

namespace EasyCommerce.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(string id);  
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(string id, Customer customer);  
        Task DeleteCustomerAsync(string id);  
    }
}
