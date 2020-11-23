using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PointOfSale.BLL.BaseObjects.RePointOfSaleitoriesInterfaces;
using PointOfSale.DAL.Domains;
using PointOfSale.Services.ISevices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Services.Sevices
{
    public class OrderService : BusinessService<Order, Order>, IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRePointOfSaleitory<Order> _rePointOfSaleitory;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _rePointOfSaleitory = _unitOfWork.GetRepository<Order>();
        }

        public async Task<Order> GetOrderByNo(string id)
        {
            return await _rePointOfSaleitory.GetAll().Include(c => c.OrderItem).ThenInclude(o => o.Product).FirstOrDefaultAsync(c => c.OrderNumber == id);
        }
    }
}
