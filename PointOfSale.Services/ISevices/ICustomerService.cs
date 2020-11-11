using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Services.ISevices
{
    public interface ICustomerService : IBusinessService<Customer, CustomerDTO>
    {
        //Task<List<Customer>> GetCustomersAsync();
        //Task<Customer> GetCustomerByIdAsync(int id);
        //Task<Customer> InsertCustomersAsync(Customer customer);
        //Customer UpdateCustomers(Customer customer);
        //bool DeleteCustomers(int id);
    }
}
