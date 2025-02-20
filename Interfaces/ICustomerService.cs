using EasyCommerce.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyCommerce.DTO;

namespace EasyCommerce.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(int id, Customer customer);
        Task DeleteCustomerAsync(int id);
    }
}
