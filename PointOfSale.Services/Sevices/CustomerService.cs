using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PointOfSale.BLL.BaseObjects.RePointOfSaleitoriesInterfaces;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using PointOfSale.Services.ISevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Services.Sevices
{
    public class CustomerService : BusinessService<Customer,CustomerDTO>, ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
       // private IRePointOfSaleitory<Customer> _rePointOfSaleitory;
        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
           // _rePointOfSaleitory = _unitOfWork.GetRepository<Customer>();
        }

        //public bool DeleteCustomers(int id)
        //{
        //    try
        //    {
        //        _rePointOfSaleitory.Delete(id);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {

        //        return false;
        //    }
        //}

        //public async Task<Customer> GetCustomerByIdAsync(int id)
        //{
        //    return await _rePointOfSaleitory.FindAsync(id);
        //}

        //public async Task<List<Customer>> GetCustomersAsync()
        //{
        //    return await _rePointOfSaleitory.GetAll().ToListAsync();
        //}

        //public async Task<Customer> InsertCustomersAsync(Customer customer)
        //{
        //    await _rePointOfSaleitory.InsertAsync(customer);
        //    return customer;
        //}

        //public Customer UpdateCustomers(Customer customer)
        //{
        //    _rePointOfSaleitory.Update(customer);
        //    return customer;
        //}
    }
}
